using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService, AuthController auth)
        {
            this._userService = userService;
            _authController = auth;
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

                return Ok(await _userService.AddUser(User));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Service to LoginUSer
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Token if User Exists else error Message</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] BLLoginModel login)
        {
            try
            {

                var userId = await _userService.Login(login);

                if (User != null)
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
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Put([FromBody] BLUser User)
        {
            try
            {
                return Ok(await _userService.UpdateUser(User));
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
        /// <returns>true if successful else false</returns>
        // DELETE api/<UserController>/5
        [HttpDelete("BlockUser")]
        public async Task<IActionResult> BlockUser(Guid UserId)
        {
            try
            {
                return Ok(await _userService.ToggleIsActiveById(UserId));
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
    }
}
