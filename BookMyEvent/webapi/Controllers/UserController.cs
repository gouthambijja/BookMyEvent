using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.WebApi.Utilities;
using db.Models;
using Microsoft.AspNetCore.Mvc;

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
        // GET: api/<UserController>
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
        [HttpGet("{Email}")]
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

        // POST api/<UserController>
        [HttpPost("AddUser")]
        public async Task<IActionResult> Post([FromBody] BLUser User)
        {
            try
            {
                return Ok(await _userService.AddUser(User));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
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
    }
}
