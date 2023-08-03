using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> _logger;
        private readonly FileLogger _fileLogger;

        public UserController(IUserService userService, AuthController auth, IConfiguration configuration, ILogger<UserController> logger, FileLogger fileLogger)
        {
            this._userService = userService;
            _authController = auth;
            _configuration = configuration;
            _logger = logger;
            _fileLogger = fileLogger;
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
                var users = await _userService.GetUsers();
                if (users != null)
                {
                    _fileLogger.AddInfoToFile("[GetAllUsers] Fetch All Active Users Success");
                    return Ok(users);
                    
                }
                else
                {
                    _fileLogger.AddExceptionToFile("[GetAllUsers] Fetcj All Users Failed");
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetAllUsers]" + ex.Message);
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
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user != null)
                {
                    _fileLogger.AddInfoToFile("[GetUserById] Get User By Id Success");
                    return Ok(user);
                }
                else
                {
                    _fileLogger.AddExceptionToFile("[GetUserById] Get User By Id Failed");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetUserById]" + ex.Message);
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
                var user = await _userService.GetUserByEmail(Email);
                if (user != null)
                {
                    _fileLogger.AddInfoToFile("[GetUserByEmail] Get User By Email Success");
                    return Ok(user);
                }
                else
                {
                    _fileLogger.AddExceptionToFile("[GetUserByEmail] User By this Email is null");

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetUserByEmail]" + ex.Message);
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
        public async Task<IActionResult> AddUser()
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
                var resp = await _userService.AddUser(User);
                if (resp.IsUserAdded)
                {
                    _fileLogger.AddInfoToFile("[AddUser] Add User Success");
                    return Ok(resp);
                }
                else
                {
                    _fileLogger.AddExceptionToFile("[AddUser]" + resp.Message);
                    return BadRequest(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[AddUser]" + ex.Message);
                return BadRequest(ex.Message);
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
                _fileLogger.AddInfoToFile("[GoogleSignUp] Google Sign Up Success");
                return Ok(JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GoogleSignUp]" + ex.Message);
                return BadRequest(ex.Message);
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


                _logger.LogInformation("my first log");
                var passwordSalt = _configuration["Encryption:PasswordSalt"];

                login.Password = HashPassword.GetHash(login.Password + passwordSalt);
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
                    _fileLogger.AddInfoToFile("[LoginUser] User Login Success");
                    return Ok(accessToken);
                }
                else
                {

                    _fileLogger.AddExceptionToFile("[LoginUser] User Login Failed");

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[LoginUser]" + ex.Message);


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
        public async Task<IActionResult> UpdateUser([FromBody] BLUser User)
        {
            try
            {
                var resp = await _userService.UpdateUser(User);
                if (resp.Item1 != null)
                {
                    _fileLogger.AddInfoToFile("[UpdateUser] User Update Success");

                    return Ok(resp.Item1);
                }
                else
                {
                    _fileLogger.AddExceptionToFile("[UpdateUser]" + resp.Message);

                    return BadRequest(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[UpdateUser]" + ex.Message);

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
        public async Task<IActionResult> ChangeUserPassword([FromBody] BLChangePassword changePassword)
        {
            try
            {
                bool isPasswordChanged = await _userService.ChangePassword(changePassword.AdminId, changePassword.Password);
                if (isPasswordChanged)
                {
                    _fileLogger.AddInfoToFile("[ChangeUserPassword] User Password Change Success");

                    return Ok(isPasswordChanged);
                }
                else
                {
                    _fileLogger.AddExceptionToFile("[ChangeUserPassword] User Password Change Failed");

                    return BadRequest(false);
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[ChangeUserPassword]" + ex.Message);

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
        [Authorize(Roles = "Admin")]
        [HttpDelete("BlockUser/{UserId}/{BlockedBy}")]
        public async Task<IActionResult> BlockUser(Guid UserId, Guid BlockedBy)
        {
            try
            {
                var user = await _userService.ToggleIsActiveById(UserId, BlockedBy);
                _fileLogger.AddInfoToFile("[BlockUser] User Block Success");

                return Ok(user);
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[BlockUser]" + ex.Message);

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
                var resp = await _userService.DeleteUser(UserId);
                if (resp.IsDeleted)
                {
                    _fileLogger.AddInfoToFile("[DeleteUser] Delete User Success");
                    return Ok(resp);

                }
                else
                {
                    _fileLogger.AddExceptionToFile("[DeleteUser]" + resp.Message);

                    return BadRequest(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[DeleteUser]" + ex.Message);
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
                _fileLogger.AddInfoToFile("[IsEmailExists] Checked for Email ");

                return Ok(new
                {
                    IsEmailTaken = result.IsUserEmailExists,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[IsEmailExists] " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetFilteredUsers")]
        public async Task<IActionResult> GetFilteredUsers(string name = null, string email = null, string phoneNumber = null, bool? isActive = null)
        {
            try
            {
                var users = await _userService.GetFilteredUsers(name, email, phoneNumber, isActive);
                if (users != null)
                {
                    _fileLogger.AddInfoToFile("[GetFilteredUsers] Fetch Filtered Users Success");

                    return Ok(users);
                }
                else
                {
                    _fileLogger.AddExceptionToFile("[GetFilteredUsers] Fetch Filtered Users Failed");

                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetFilteredUsers]" + ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
