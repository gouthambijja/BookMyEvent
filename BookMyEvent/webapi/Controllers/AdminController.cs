using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.WebApi.Utilities;
using db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly AuthController _authController;
        public AdminController(IAdminService adminService, AuthController auth)
        {
            _adminService = adminService;
            _authController = auth;
        }
        /// <summary>
        /// Used for AdminLogin
        /// </summary>
        /// <param name="login"></param>
        /// <returns>returns token if user exists else message as "user Not found"</returns>
        [AllowAnonymous]
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
        /// <summary>
        /// It is for Getting full details of Admin by AdminId
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns>returns Admin Object</returns>
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
        /// <summary>
        /// Returns all the admins created by specific Admin
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns>list of Admins Created by specific Admin</returns>
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
        /// <summary>
        /// Used to Add Admin
        /// </summary>
        /// <returns>Retuns Added Admin Details</returns>
        [HttpPost("AddAdmin")]
        
        public async Task<IActionResult> AddAdmin()
        {
            try
            {

                var image = Request.Form.Files[0];
                var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var imageBody = memoryStream.ToArray();
                var Admin = new BLAdministrator()
                {
                    AdministratorName = Request.Form.Where(e => e.Key == "administratorName").First().Value,
                    AdministratorAddress = Request.Form.Where(e => e.Key == "administratorAddress").First().Value,
                    Email = Request.Form.Where(e => e.Key == "email").First().Value,
                    PhoneNumber = Request.Form.Where(e => e.Key == "phoneNumber").First().Value,
                    CreatedOn = DateTime.Parse(Request.Form.Where(e => e.Key == "createdOn").First().Value),
                    UpdatedOn = DateTime.Parse(Request.Form.Where(e => e.Key == "updatedOn").First().Value),
                    RoleId = byte.Parse(Request.Form.Where(e => e.Key == "roleId").First().Value),
                    IsAccepted = bool.Parse(Request.Form.Where(e => e.Key == "isAccepted").First().Value),
                    ImageName = Request.Form.Where(e => e.Key == "imageName").First().Value,
                    CreatedBy = Guid.Parse(Request.Form.Where(e => e.Key == "createdBy").First().Value),
                    AcceptedBy = Guid.Parse(Request.Form.Where(e => e.Key == "acceptedBy").First().Value),
                    OrganisationId = Guid.Parse(Request.Form.Where(e => e.Key == "organisationId").First().Value),
                    IsActive = bool.Parse(Request.Form.Where(e => e.Key == "isActive").First().Value),
                    Password = Request.Form.Where(e => e.Key == "password").First().Value,
                    ImgBody = imageBody


                };
                BLAdministrator result = await _adminService.CreateAdministrator(Admin);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Update Admin
        /// </summary>
        /// <param name="Admin"></param>
        /// <returns>returns Updated Admin details</returns>
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
        /// <summary>
        /// Service to Change Admin Password
        /// </summary>
        /// <param name="ChangePassword"></param>
        /// <returns>true If changes Successfully else false</returns>
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