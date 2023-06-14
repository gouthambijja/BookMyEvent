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
        [HttpPost("RegisterOwner")]
        public async Task<IActionResult> RegisterOrganiserOwner(OrganiserRegistration registration)
        {
            //BLAdministrator profile, BLOrganisation organisation
            //var result = await _organiserServices.RegisterOwner(organiser.profile, organiser.organisation);

            var result = await _organiserServices.RegisterOwner(registration.Organiser, registration.Organisation);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }

        [HttpPost("RegisterPeer")]
        public async Task<IActionResult> RegisterOrganiserPeer(BLAdministrator peer)
        {
            var result = await _organiserServices.RegisterPeer(peer);
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

        [HttpGet("{id}")]
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

        [HttpPut("{id}/Accept")]
        public async Task<IActionResult> AcceptOrganiser(BLAdministrator administrator)
        {
            var result = await _organiserServices.AcceptOrganiser(administrator.AdministratorId, administrator.AcceptedBy);
            if (result)
                return Ok("Accepted Successfully");
            else return BadRequest("Accept failed");
        }

        //[HttpPut("{id}/Reject")]
        //public async Task<IActionResult> RejectOrganiser(BLAdministrator administrator)
        //{
        //    var result = await _organiserServices.RejectOrganiser(administrator.AdministratorId, administrator.RejectedBy, administrator.Reason);
        //    if (result)
        //        return Ok("Rejected Successfully");
        //    else return BadRequest("Reject failed");
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganiser(BLAdministrator administrator)
        {
            var result = await _organiserServices.UpdateOrganiser(administrator);
            if (result is not null)
                return Ok(result);
            else return BadRequest("Update failed");
        }

        [HttpDelete("{id}/DeleteBy/{deletedById}")]
        public async Task<IActionResult> DeleteOrganiser(Guid id, Guid deletedById)
        {
            var result = await _organiserServices.BlockOrganiser(id, deletedById);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }

        [HttpDelete("Organisation/{id}/BlockedBy/{blockerId}")]
        public async Task<IActionResult> DeleteAllOrganisationOrganisers(Guid id, Guid blockerId)
        {
            var result = await _organiserServices.BlockAllOrganisationOrganisers(id, blockerId);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }

        [HttpGet("Organisation/{id}")]
        public async Task<IActionResult> GetOrganisationOrganisers(Guid id)
        {
            var result = await _organiserServices.GetAllOrganisationOrganisers(id);
            if (result is not null)
                return Ok(result);
            else return BadRequest("No organisers found");
        }

        [HttpGet("IsEmailTaken")]
        public async Task<IActionResult> IsEmailTaken(string email)
        {
            var result = await _organiserServices.IsOrganiserAvailableWithEmail(email);
            return Ok(new
            {
                IsEmailTaken = result.IsOrganiserEmailAvailable,
                Message = result.Message
            }) ;
        }

        [HttpGet("AllOwners")]
        public async Task<IActionResult> GetAllOwners()
        {
            var result = await _organiserServices.GetAllOwners();
            return Ok(result);
        }
    }
}