using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookMyEvent.WebApi.Utilities;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        internal static ConfigurationManager _config;
        //internal readonly IAdminService _adminService;
        public static void config(ConfigurationManager config)
        {
            _config = config;
        }
        //public AuthController(IAdminService adminService)
        //{
        //    _adminService = adminService;
        //}
        //jwt methods//---------------------------------
        internal string GenerateJwtToken(string email,Guid Id, string role,TokenType tokenType)
        {
            DateTime expiresIn;
            string secretKey = string.Empty;
            if (tokenType == TokenType.AccessToken)
            {
                secretKey = _config["Authentication:JWTSettings:AccessTokenSecretKey"];
                expiresIn = DateTime.Now.AddMinutes(5);
            }
            else
            {
                secretKey = _config["Authentication:JWTSettings:RefreshTokenSecretKey"];
                expiresIn = DateTime.Now.AddMonths(2);
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            var claimUsername = new Claim(ClaimTypes.Email, email);
            //converting Guid to string and using it as claimIdentifier
            var claimIdentifier = new Claim(ClaimTypes.NameIdentifier, Id.ToString());
            var claimRole = new Claim(ClaimTypes.Role, role);

            var claimIdentity = new ClaimsIdentity(new[] { claimUsername, claimIdentifier, claimRole }, "serverAuth");

            


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //
                Subject = claimIdentity,
                //expire in next x days
                Expires = expiresIn,
                //which 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //internal string GenerateJwtRefreshToken(string email, Guid Id, string role)
        //{
        //    string secretKey = string.Empty;
        //    secretKey = _config["Authentication:JWTSettings:RefreshTokenSecretKey"];

        //    var key = Encoding.ASCII.GetBytes(secretKey);

        //    var claimUsername = new Claim(ClaimTypes.Email, email);
        //    var claimIdentifier = new Claim(ClaimTypes.NameIdentifier, Id.ToString());
        //    var claimRole = new Claim(ClaimTypes.Role, role);

        //    var claimIdentity = new ClaimsIdentity(new[] { claimUsername, claimIdentifier, claimRole }, "serverAuth");

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        //
        //        Subject = claimIdentity,
        //        //expire in next x days
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        //which 
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    //creating a token handler
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        [HttpPost("authenticatejwt")]
        public async Task<string> Authenticatejwt()
        {
            
            string token = string.Empty;
            //token = GenerateJwtToken(user);
            Console.WriteLine(token);
            return  token;
        }
        //public async Task<ActionResult<LoginCredintials>> GetUserByJwt([FromBody] string jwtToken)
      

        [HttpPost("getNewAccessTokenUsingRefreshToken")]
        public async Task<dynamic> FetNewAccessTokenUsingRefreshToken()
        {
            try
            {
                var RefreshToken = Request.Cookies["RefreshToken"];
                //getting the secret key
                string secretKey = _config["Authentication:JWTSettings:RefreshTokenSecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);

                //preparing the validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                Console.WriteLine(RefreshToken);
                //validationg token
                var principle = tokenHandler.ValidateToken(RefreshToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var role = principle.FindFirst(ClaimTypes.Role)?.Value;
                    var email = principle.FindFirst(ClaimTypes.Email)?.Value;
                    var Id = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return GenerateJwtToken(email, Guid.Parse(Id),role,TokenType.AccessToken);
                }
                else
                {
                    //here if the token is not valid then a null is sent back
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return null;
        }
        //[HttpPost("getuserbyjwt")]
        //public async Task<BLUser> GetUserByJwtPost(string jwtToken)
        //{
        //    Console.Write("hello");

        //    try
        //    {
        //        //getting the secret key
        //        string secretKey = _config["Authentication:JWTSettings:SecretKey"];
        //        var key = Encoding.ASCII.GetBytes(secretKey);

        //        //preparing the validation parameters
        //        var tokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        SecurityToken securityToken;

        //        Console.WriteLine(jwtToken);
        //        //validationg token
        //        var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
        //        var jwtSecurityToken = (JwtSecurityToken)securityToken;

        //        if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //            return await _userService.GetUserById(userId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return null;
        //    }
        //    return null;
        //}

    }
}
