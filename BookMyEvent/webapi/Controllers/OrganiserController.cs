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
        /// <summary>
        /// Service to Register OrganizationOwner
        /// </summary>
        /// <returns>OrganizationOwner Details</returns>
        [HttpPost("RegisterOwner")]
        public async Task<IActionResult> RegisterOrganiserOwner()
        {
            var image = Request.Form.Files[0];

            var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var imageBody = memoryStream.ToArray();
            var owner = new BLAdministrator()
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
      
                IsActive = bool.Parse(Request.Form.Where(e => e.Key == "isActive").First().Value),
                Password = Request.Form.Where(e => e.Key == "password").First().Value,
                ImgBody = imageBody


            };
            var organisation = new BLOrganisation()
            {
                OrganisationName = Request.Form.Where(e => e.Key == "organisationName").First().Value,
               OrganisationDescription = Request.Form.Where(e => e.Key == "organisationDescription").First().Value,
                Location = Request.Form.Where(e => e.Key == "Location").First().Value,
                CreatedOn =  DateTime.Parse(Request.Form.Where(e => e.Key == "orgcreatedOn").First().Value),
                IsActive = bool.Parse(Request.Form.Where(e => e.Key == "orgIsActive").First().Value),
                UpdatedOn =  DateTime.Parse(Request.Form.Where(e => e.Key == "orgUpdatedOn").First().Value),
                
            };

            var result = await _organiserServices.RegisterOwner(owner, organisation);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        /// <summary>
        /// Service to Register Organization Peer
        /// </summary>
        /// <returns>Peer Details</returns>
        [HttpPost("RegisterPeer")]
        public async Task<IActionResult> RegisterOrganiserPeer()
        {
            
            var image = Request.Form.Files[0];
           
            var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var imageBody = memoryStream.ToArray();
            var peer = new BLAdministrator()
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
               
                OrganisationId = Guid.Parse(Request.Form.Where(e => e.Key == "organisationId").First().Value),
                IsActive = bool.Parse(Request.Form.Where(e => e.Key == "isActive").First().Value),
                Password = Request.Form.Where(e => e.Key == "password").First().Value,
                ImgBody = imageBody


            };
            var result = await _organiserServices.RegisterPeer(peer);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        /// <summary>
        /// Service toCreate Secondary Organizer
        /// </summary>
        /// <returns>Secondary Organization Details</returns>
        [HttpPost("CreateOrganiser")]
        public async Task<IActionResult> CreateSecondaryOrganiser()
        {
            var image = Request.Form.Files[0];
            var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var imageBody = memoryStream.ToArray();
            var secondaryOwner = new BLAdministrator()
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
        /// <summary>
        /// Service to Login Organizer
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Returns The Token if User Exists</returns>
        [HttpPost("LoginOrganiser")]
        public async Task<IActionResult> LoginOrganiser(BLLoginModel login)
        {
            var result = await _organiserServices.LoginOrganiser(login.Email, login.Password);
            if (result.IsSuccessfull)
            {
                string role = result.roleId == 2 ? Roles.Owner.ToString() : result.roleId == 3 ? Roles.Secondary_Owner.ToString() : Roles.Peer.ToString();
                var accessToken = _authController.GenerateJwtToken(login.Email, result.administratorId, role, TokenType.AccessToken);
                string refreshToken = _authController.GenerateJwtToken(login.Email, result.administratorId, role, TokenType.RefreshToken);
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
        /// <summary>
        /// Service to Get Organizer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Organization Details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganiserById(Guid id)
        {
            var result = await _organiserServices.GetOrganiserById(id);
            if (result is not null)
                return Ok(result);
            else return BadRequest("Organiser not found");
        }
        /// <summary>
        /// Service to Get all The Requested Owners
        /// </summary>
        /// <returns>List of All Requested Owners</returns>
        [HttpGet("RequestedOwners")]
        public async Task<IActionResult> GetRequestedOwners()
        {
            var result = await _organiserServices.GetAllRequestedOwners();
            if (result is not null)
                return Ok(result);
            else return Ok("No requests found");
        }
        /// <summary>
        /// Service to Get Requested Peers
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>List of Peers</returns>
        [HttpGet("Organisation/{orgId}/RequestedPeers")]
        public async Task<IActionResult> GetRequestedPeers(Guid orgId)
        {
            var result = await _organiserServices.GetAllRequestedOrganisers(orgId);
            if (result is not null)
                return Ok(result);
            else return Ok("No requests found");
        }
        /// <summary>
        /// Service to Accept Organizer
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns>Message About Accepted or Not</returns>
        [HttpPut("{id}/Accept")]
        public async Task<IActionResult> AcceptOrganiser(BLAdministrator administrator)
        {
                Console.WriteLine(administrator.RoleId);

            var result = await _organiserServices.AcceptOrganiser(administrator.AdministratorId, administrator.AcceptedBy,administrator.RoleId,administrator.OrganisationId);
            if (result)
                return Ok("Accepted Successfully");
            else return BadRequest("Accept failed");
        }
        /// <summary>
        /// Service to Reject Organizer 
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns>Message Of rejected Organizer</returns>
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> rejectorganiser(BLAdministrator? administrator)
        {
            var result = await _organiserServices.RejectOrganiser(administrator.AdministratorId, administrator.RejectedBy);
            if (result)
                return Ok("rejected successfully");
            else return BadRequest("reject failed");
        }
        /// <summary>
        /// Service to Update Organizer
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns>Updated Organization Details</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganiser(BLAdministrator administrator)
        {
            var result = await _organiserServices.UpdateOrganiser(administrator);
            if (result is not null)
                return Ok(result);
            else return BadRequest("Update failed");
        }
        /// <summary>
        /// Service to Delete an Organizer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedById"></param>
        /// <returns>Message about Deleted or not</returns>
        [HttpDelete("{id}/DeleteBy/{deletedById}")]
        public async Task<IActionResult> DeleteOrganiser(Guid id, Guid deletedById)
        {
            var result = await _organiserServices.BlockOrganiser(id, deletedById);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        /// <summary>
        /// Service to Delete All the Organization Organizers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="blockerId"></param>
        /// <returns>Message about deleted or not</returns>
        [HttpDelete("Organisation/{id}/BlockedBy/{blockerId}")]
        public async Task<IActionResult> DeleteAllOrganisationOrganisers(Guid id, Guid blockerId)
        {
            var result = await _organiserServices.BlockAllOrganisationOrganisers(id, blockerId);
            if (result.IsSuccessfull)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        /// <summary>
        /// Service to 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Organisation/{id}")]
        public async Task<IActionResult> GetOrganisationOrganisers(Guid id)
        {
            var result = await _organiserServices.GetAllOrganisationOrganisers(id);
            if (result is not null)
                return Ok(result);
            else return BadRequest("No organisers found");
        }
        /// <summary>
        /// Service to Check Email Is Valid or Not
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if valid else false</returns>
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
        /// <summary>
        /// Service to Get All Owners 
        /// </summary>
        /// <returns>List of all the Owners</returns>
        [HttpGet("AllOwners")]
        public async Task<IActionResult> GetAllOwners()
        {
            var result = await _organiserServices.GetAllOwners();
            return Ok(result);
        }

        
    }
}