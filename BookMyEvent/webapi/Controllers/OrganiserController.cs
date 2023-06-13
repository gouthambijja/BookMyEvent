using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using BookMyEvent.WebApi.Utilities;



namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserController : ControllerBase
    {
        private readonly IOrganiserServices _organiserServices;
        private readonly AuthController _authController;
        public OrganiserController(IOrganiserServices organiserServices, AuthController authController)
        {
            _organiserServices = organiserServices;
            _authController = authController;
        }
        [HttpPost("RegisterOrganiser")]
        public async Task<IActionResult> RegisterOrganiserOwner(OrganiserRegistration registration)
        {
            //BLAdministrator profile, BLOrganisation organisation
            //var result = await _organiserServices.RegisterOwner(organiser.profile, organiser.organisation);

            var result = await _organiserServices.RegisterOwner(registration.Organiser, registration.Organisation);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }

        [HttpPost("CreateOrganiser")]
        public async Task<IActionResult> CreateSecondaryOrganiser(BLAdministrator secondaryOwner)
        {
            var result = await _organiserServices.CreateSecondaryOwner(secondaryOwner);
            Console.WriteLine(result.Message);
            var response = new
            {
                IsSuccess = result.IsSuccessfull,
                Message = result.Message
            };
            if (result.IsSuccessfull)
                return Ok(new
                {
                    IsSuccess = result.IsSuccessfull,
                    Message = result.Message
                });
            else return BadRequest(new
            {
                IsSuccess = new BLAdministrator(),
                Message = result.Message
            });
        }

        [HttpPost("LoginOrganiser")]
        public async Task<IActionResult> LoginOrganiser(BLLoginModel login)
        {
            var result = await _organiserServices.LoginOrganiser(login.Email, login.Password);
            if (result.IsSuccessfull)
            {
                var accessToken = _authController.GenerateJwtToken(login.Email, result.administratorId, result.roleId.ToString(), TokenType.AccessToken);
                string refreshToken = _authController.GenerateJwtToken(login.Email, result.administratorId, result.roleId.ToString(), TokenType.RefreshToken);
                Response.Cookies.Append(
                            "RefreshToken",
                            refreshToken,
                            new CookieOptions
                            {
                                HttpOnly = true
                            });
                return Ok(new
                {

                    IsSuccess = result.IsSuccessfull,
                    Message = result.Message,
                    AccessToken = accessToken
                });
            }
            else return BadRequest(new
            {
                IsSuccess = result.IsSuccessfull,
                Message = result.Message,
            });
        }

        [HttpGet("Organiser/{id}")]
        public async Task<IActionResult> GetOrganiserById(Guid id)
        {
            var result = await _organiserServices.GetOrganiserById(id);
            if (result is not null)
                return Ok(result);
            else return BadRequest("Organiser not found");
        }

        [HttpGet("RequestedOwners")]
        public async Task<IActionResult> GetRequestedOwners()
        {
            var result = await _organiserServices.GetAllRequestedOwners();
            if (result is not null)
                return Ok(result);
            else return Ok("No requests found");
        }

        [HttpGet("Organisation/{orgId}/RequestedPeers")]
        public async Task<IActionResult> GetRequestedPeers(Guid orgId)
        {
            var result = await _organiserServices.GetAllRequestedOrganisers(orgId);
            if (result is not null)
                return Ok(result);
            else return Ok("No requests found");
        }

        //[HttpPut("Organiser/{id}/Accept")]

    }
}