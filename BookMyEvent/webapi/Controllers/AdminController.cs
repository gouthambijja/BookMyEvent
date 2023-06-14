using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
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
            try
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
                    return "User Not Found";
                }
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }
        }
        [HttpGet("AdminById")]
        public async Task<IActionResult> GetAdminById(Guid AdminId)
        {
            try
            {
                return Ok(await _adminService.GetAdminById(AdminId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("AdminsCreatedByAdmin")]
        public async Task<IActionResult> AdminsCreatedByAdminId(Guid AdminId)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(BLAdministrator Admin)
        {
            try
            {
                Console.WriteLine(Admin.AdministratorAddress);
                var image = Request.Form.Files[0];
                var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var imageBody = memoryStream.ToArray();
                Admin.ImgBody= imageBody;
                BLAdministrator result = await _adminService.CreateAdministrator(Admin);
                var id = result.RoleId;
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(BLAdministrator Admin)
        {
            try
            {
                return Ok(await _adminService.UpdateAdministrator(Admin));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("ChangeAdminPassword")]
        public async Task<IActionResult> ChangePassword(BLChangePassword ChangePassword)
        {
            try
            {
                return Ok(await _adminService.ChangeAdminPassword(ChangePassword.AdminId, ChangePassword.Password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}