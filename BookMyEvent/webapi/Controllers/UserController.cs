using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        
       
        // GET: api/<UserController>
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _userService.GetUserById(id));
        }
        [HttpGet("{Email}")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            return Ok(await _userService.GetUserByEmail(Email));
        }

        // POST api/<UserController>
        [HttpPost("AddUser")]
        public async Task<IActionResult> Post([FromBody] BLUser User)
        {
            if (User is not null)
            {
                return Ok(await _userService.AddUser(User));
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] BLLoginModel login)
        {
            if(login is not null)
            {
                return Ok(await _userService.Login(login));
            }
            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Put([FromBody] BLUser User)
        {
            if (User is not null)
            {
                return Ok(await _userService.UpdateUser(User));
            }
            return BadRequest();
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] BLChangePassword changePassword)
        {
            if (changePassword is not null)
            {
                return Ok(await _userService.ChangePassword(changePassword.AdminId,changePassword.Password));
            }
            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("BlockUser")]
        public async Task<IActionResult> BlockUser(Guid UserId)
        {
            if (UserId != null)
            {
                return Ok(await _userService.ToggleIsActiveById(UserId));
            }
            return BadRequest();
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid UserId)
        {
            if(User != null)
            {
                return Ok(await _userService.DeleteUser(UserId));
            }
            return BadRequest();
        }
    }
}
