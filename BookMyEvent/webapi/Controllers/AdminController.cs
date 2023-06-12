using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly AuthController _authController;
        public AdminController(IAdminService adminService, AuthController auth)
        {
            _adminService = adminService;
            _authController = auth;
        }
        [HttpPost("loginAdmin")]
        public async Task<string> LoginAdmin([FromBody] BLLoginModel login)
        {
            var user = await _adminService.LoginAdmin(login.Email, login.Password, Roles.Admin.ToString());
            if (user != null)
            {
                Console.WriteLine(user.AdministratorAddress);
                var accessToken = _authController.GenerateJwtToken(login.Email, user.AdministratorId, Roles.Admin.ToString(), TokenType.AccessToken);
                string refreshToken = _authController.GenerateJwtToken(login.Email, user.AdministratorId, Roles.Admin.ToString(), TokenType.RefreshToken);
                Response.Cookies.Append(
                            "RefreshToken",
                            refreshToken,
                            new CookieOptions
                            {
                                HttpOnly = true
                            });
                return accessToken;
            }
            else
            {
                return null;
            }
        }
        [HttpGet("AdminById")]
        public async Task<IActionResult> GetAdminById(Guid AdminId)
        {
            return Ok(await _adminService.GetAdminById(AdminId));
        }
        [HttpGet("AdminsCreatedByAdmin")]
        public async Task<IActionResult> AdminsCreatedByAdminId(Guid AdminId)
        {
            if (AdminId != null)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(BLAdministrator Admin)
        {
            if (Admin == null)
            {
                return BadRequest();
            }
            return Ok(_adminService.UpdateAdministrator(Admin));
        }
        [HttpPut("ChangeAdminPassword")]
        public async Task<IActionResult> ChangePassword(BLChangePassword ChangePassword)
        {
            return Ok(await _adminService.ChangeAdminPassword(ChangePassword.AdminId, ChangePassword.Password));
        }

    }
}