using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookMyEvent.BLL.RequestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthController _authController;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, AuthController auth, IConfiguration configuration)
        {
            this._userService = userService;
            _authController = auth;
            _configuration = configuration;
        }
        [Authorize]
        [HttpGet("getFakestring")]
        public IActionResult Get()
        {
            return Ok("faekString");
        }
        // GET: api/<UserController>
        /// <summary>
        /// Service to Get All The Users
        /// </summary>
        /// <returns>List of Users</returns>
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Get a User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<UserController>/5
        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _userService.GetUserById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Get A User By Email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>User Details </returns>
        [HttpGet("user/{Email}")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            try
            {
                return Ok(await _userService.GetUserByEmail(Email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Post A user
        /// </summary>
        /// <returns>true if added succesfully else false along with the confirmatin Message</returns>
        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost("AddUser")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var image = Request.Form.Files[0];

                var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var imageBody = memoryStream.ToArray();
                var User = new BLUser()
                {
                    Name = Request.Form.Where(e => e.Key == "Name").First().Value,
                    Email = Request.Form.Where(e => e.Key == "email").First().Value,
                    PhoneNumber = Request.Form.Where(e => e.Key == "phoneNumber").First().Value,
                    UserAddress = Request.Form.Where(e => e.Key == "userAddress").First().Value,
                    CreatedOn = DateTime.Parse(Request.Form.Where(e => e.Key == "createdOn").First().Value),
                    UpdatedOn = DateTime.Parse(Request.Form.Where(e => e.Key == "updatedOn").First().Value),
                    ImageName = Request.Form.Where(e => e.Key == "imageName").First().Value,
                    IsActive = bool.Parse(Request.Form.Where(e => e.Key == "isActive").First().Value),
                    Password = Request.Form.Where(e => e.Key == "password").First().Value,
                    ImgBody = imageBody

                };
                var passwordSalt = _configuration["Encryption:PasswordSalt"];

                User.Password = HashPassword.GetHash(User.Password + passwordSalt);
                Console.WriteLine(User.Password);
                return Ok(await _userService.AddUser(User));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Registering user with googleID and credentials
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GoogleSignUp")]
        public async Task<IActionResult> GoogleSignUp([FromBody] BLUser? user)
        {
            try
            {
                user.Password = user.GoogleId;
                var passwordSalt = _configuration["Encryption:PasswordSalt"];
                user.Password = HashPassword.GetHash(user.Password + passwordSalt);
                var data = await _userService.AddUser(user);
                return Ok(JsonConvert.SerializeObject(data));
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Service to LoginUSer
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Token if User Exists else error Message</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] BLLoginModel login)
        {
            try
            {
                var passwordSalt = _configuration["Encryption:PasswordSalt"];
                
                login.Password=HashPassword.GetHash(login.Password+passwordSalt);
                Console.WriteLine(login.Password);
                var userId = await _userService.Login(login);

                if (userId != Guid.Empty)
                {
                    var accessToken = _authController.GenerateJwtToken(login.Email, userId, Roles.User.ToString(), TokenType.AccessToken);
                    string refreshToken = _authController.GenerateJwtToken(login.Email, userId, Roles.User.ToString(), TokenType.RefreshToken);
                    Response.Cookies.Append(
                                "RefreshToken",
                                refreshToken,
                                new CookieOptions
                                {
                                    HttpOnly = true
                                });
                    return Ok(accessToken);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Update User
        /// </summary>
        /// <param name="User"></param>
        /// <returns>Updated User Details</returns>
        // PUT api/<UserController>/5
        [Authorize(Roles = "User")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Put([FromBody] BLUser User)
        {
            try
            {
                var resp=await _userService.UpdateUser(User);
                if(resp.Item1 != null)
                {
                    return Ok(resp.Item1);
                }
                else
                {
                    return BadRequest(resp.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Change User Password
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns>true if Successful else false</returns>
        [Authorize(Roles = "User")]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] BLChangePassword changePassword)
        {
            try
            {
                return Ok(await _userService.ChangePassword(changePassword.AdminId, changePassword.Password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Block the User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="BlockedBy"></param>
        /// <returns>true if successful else false</returns>
        // DELETE api/<UserController>/5
        [Authorize(Roles="Admin")]
        [HttpDelete("BlockUser/{UserId}/{BlockedBy}")]
        public async Task<IActionResult> BlockUser(Guid UserId, Guid BlockedBy)
        {
            try
            {
                return Ok(await _userService.ToggleIsActiveById(UserId, BlockedBy));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Delete User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>true if successful else false</returns>
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid UserId)
        {
            try
            {
                return Ok(await _userService.DeleteUser(UserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to verify Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if Valid else false</returns>
        [AllowAnonymous]
        [HttpGet("isEmailExists/{email}")]
        public async Task<IActionResult> IsEmailExists(string email)
        {
            try
            {
                var result = await _userService.IsUserAvailableWithEmail(email);
                return Ok(new
                {
                    IsEmailTaken = result.IsUserEmailExists,
                    Message = result.Message
                });
            }
            catch
            {
                return BadRequest("error");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetFilteredUsers")]
        public async Task<IActionResult> GetFilteredUsers(string name = null, string email = null, string phoneNumber = null, bool? isActive = null)
        {
            try
            {
                Console.WriteLine("Im in controller ________________________________________________________________________________________________________________________________________________________");
                return Ok(await _userService.GetFilteredUsers(name, email, phoneNumber, isActive));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
