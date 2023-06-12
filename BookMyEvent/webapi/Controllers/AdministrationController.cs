﻿using BookMyEvent.BLL.Contracts;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly AuthController _authController;
        public AdministrationController(IAdminService adminService,AuthController auth)
        {
            _adminService = adminService;
            _authController = auth;
        }
        [HttpPost("loginAdmin")]
        public async Task<string> LoginAdmin(string email, string password)
        {
            var user = await _adminService.LoginAdmin(email, password, Roles.Admin.ToString());
            if (user != null)
            {
                Console.WriteLine(user.AdministratorAddress);
                var accessToken = _authController.GenerateJwtToken(email, user.AdministratorId, Roles.Admin.ToString(),TokenType.AccessToken);
                string refreshToken = _authController.GenerateJwtToken(email, user.AdministratorId, Roles.Admin.ToString(),TokenType.RefreshToken);
                Response.Cookies.Append(
                            "RefreshToken",
                            refreshToken,
                            new CookieOptions
                            {
                                HttpOnly = true
                            });
                return accessToken;
            }else
            {
                return null;
            }
        }
    }
}
