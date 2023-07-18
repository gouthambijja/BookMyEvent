using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Peer,Secondary_Owner,Admin")]
    public class OrganiserController : ControllerBase
    {
        private readonly IOrganiserServices _organiserServices;
        private readonly AuthController _authController;
        private readonly IConfiguration _configuration;
        private readonly FileLogger _fileLogger;
        public OrganiserController(IOrganiserServices organiserServices, AuthController authController, IConfiguration configuration, FileLogger fileLogger)
        {
            _organiserServices = organiserServices;
            _authController = authController;
            _configuration = configuration;
            _fileLogger = fileLogger;
        }
        /// <summary>
        /// Service to Register OrganizationOwner
        /// </summary>
        /// <returns>OrganizationOwner Details</returns>
        [AllowAnonymous]
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
            var passwordSalt = _configuration["Encryption:PasswordSalt"];
            owner.Password = HashPassword.GetHash(owner.Password + passwordSalt);
            var organisation = new BLOrganisation()
            {
                OrganisationName = Request.Form.Where(e => e.Key == "organisationName").First().Value,
                OrganisationDescription = Request.Form.Where(e => e.Key == "organisationDescription").First().Value,
                Location = Request.Form.Where(e => e.Key == "Location").First().Value,
                CreatedOn = DateTime.Parse(Request.Form.Where(e => e.Key == "orgcreatedOn").First().Value),
                IsActive = bool.Parse(Request.Form.Where(e => e.Key == "orgIsActive").First().Value),
                UpdatedOn = DateTime.Parse(Request.Form.Where(e => e.Key == "orgUpdatedOn").First().Value),

            };

            var result = await _organiserServices.RegisterOwner(owner, organisation);
            if (result.IsSuccessfull)
            {
                _fileLogger.AddInfoToFile("[RegisterOrganiserOwner]" + result.Message);
                return Ok(result.Message);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[RegisterOrganiserOwner]" + result.Message);

                return BadRequest(result.Message);
            }
        }
        /// <summary>
        /// Service to Register Organization Peer
        /// </summary>
        /// <returns>Peer Details</returns>
        [AllowAnonymous]
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
            var passwordSalt = _configuration["Encryption:PasswordSalt"];
            peer.Password = HashPassword.GetHash(peer.Password + passwordSalt);
            var result = await _organiserServices.RegisterPeer(peer);
            if (result.IsSuccessfull)
            {
                _fileLogger.AddInfoToFile("[RegisterOrganiserPeer]" + result.Message);

                return Ok(result.Message);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[RegisterOrganiserPeer]" + result.Message);

                return BadRequest(result.Message);
            }
        }
        /// <summary>
        /// Service toCreate Secondary Organizer
        /// </summary>
        /// <returns>Secondary Organization Details</returns>
        [AllowAnonymous]
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
            var passwordSalt = _configuration["Encryption:PasswordSalt"];
            secondaryOwner.Password = HashPassword.GetHash(secondaryOwner.Password + passwordSalt);
            var result = await _organiserServices.CreateSecondaryOwner(secondaryOwner);
            Console.WriteLine(result.Message);
            var response = new
            {
                IsSuccess = result.IsSuccessfull,
                Message = result.Message
            };
            if (result.IsSuccessfull)
            {
                _fileLogger.AddInfoToFile("[CreateSecondaryOrganiser]" + result.Message);

                return Ok(new
                {
                    IsSuccess = result.IsSuccessfull,
                    Message = result.Message
                });
            }
            else
            {
                _fileLogger.AddExceptionToFile("[CreateSecondaryOrganiser]" + result.Message);

                return BadRequest("error");
            }
        }
        /// <summary>
        /// Service to Login Organizer
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Returns The Token if User Exists</returns>
        [AllowAnonymous]
        [HttpPost("LoginOrganiser")]
        public async Task<IActionResult> LoginOrganiser(BLLoginModel login)
        {
            var passwordSalt = _configuration["Encryption:PasswordSalt"];
            login.Password = HashPassword.GetHash(login.Password + passwordSalt);
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
                _fileLogger.AddInfoToFile("[LoginOrganiser]" + result.Message);

                return Ok(new
                {

                    IsSuccess = result.IsSuccessfull,
                    Message = result.Message,
                    AccessToken = accessToken
                });
            }
            else
            {
                _fileLogger.AddExceptionToFile("[LoginOrganiser]" + result.Message);

                return BadRequest(new
                {
                    IsSuccess = result.IsSuccessfull,
                    Message = result.Message,
                });
            }
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
            {
                _fileLogger.AddInfoToFile("[GetOrganiserById] Fetch Organiser By Id Success");

                return Ok(result);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[GetOrganiserById] Fetch Organiser By Id Failed");
                return BadRequest("Organiser not found");
            }
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
            {
                _fileLogger.AddInfoToFile("[GetRequestedOwners] Fetch Owner Requests success");
                return Ok(result);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[GetRequestedOwners] Fetch Owner Requests Failed");
                return BadRequest("No requests found");
            }
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
            {
                _fileLogger.AddInfoToFile("[GetRequestedPeers] Fetch Peer Requests success");

                return Ok(result);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[GetRequestedPeers] Fetch Peer Requests Failed");

                return BadRequest("No requests found");
            }
        }
        [HttpGet("Organisation/{orgId}/NoOfRequestedPeers")]
        public async Task<IActionResult> GetNoOfRequestedPeers(Guid orgId)
        {
            try
            {

            var result = await _organiserServices.GetNoOfRequestedOrganisers(orgId);
                _fileLogger.AddInfoToFile("[GetNoOfRequestedPeers] Fetch No Of Peer Requests success");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetNoOfRequestedPeers]"+ex.Message);

                return BadRequest(ex.Message);
            }

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

            var result = await _organiserServices.AcceptOrganiser(administrator.AdministratorId, administrator.AcceptedBy, administrator.RoleId, administrator.OrganisationId);
            if (result)
            {
                _fileLogger.AddInfoToFile("[AcceptOrganiser] Organiser Accept Success");
                return Ok("Accepted Successfully");
            }
            else
            {
                _fileLogger.AddExceptionToFile("[AcceptOrganiser] Organiser Accept Failed");
                return BadRequest("Accept failed");
            }
        }
        /// <summary>
        /// Service to Reject Organizer 
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns>Message Of rejected Organizer</returns>
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> rejectorganiser(BLAdministrator? administrator)
        {
            var result = await _organiserServices.RejectOrganiser(administrator.AdministratorId, administrator.RejectedBy, administrator.RejectedReason);
            if (result)
            {
                _fileLogger.AddInfoToFile("[RejectOrganiser] Organiser Reject Success");
                return Ok("rejected successfully");
            }
            else
            {
                _fileLogger.AddExceptionToFile("[RejectOrganiser] Organiser Reject Failed");
                return BadRequest("reject failed");
            }
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
            {
                _fileLogger.AddInfoToFile("[UpdateOrganiser] Organiser Update Success");
                return Ok(result);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[UpdateOrganiser] Organiser Update Failed");

                return BadRequest("Update failed");
            }
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
            {
                _fileLogger.AddInfoToFile("[DeleteOrganiser]" + result.Message);
                return Ok(result.Message);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[DeleteOrganiser]" + result.Message);

                return BadRequest(result.Message);
            }
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
            {
                _fileLogger.AddInfoToFile("[DeleteAllOrganisationOrganisers]" + result.Message);
                return Ok(result.Message);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[DeleteAllOrganisationOrganisers]" + result.Message);

                return BadRequest(result.Message);
            }
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
            {
                _fileLogger.AddInfoToFile("[GetOrganisationOrganisers] Fetch All Active Organisation Organisers Success");

                return Ok(result);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[GetOrganisationOrganisers] Fetch All Active Organisation Organisers Failed");
                return BadRequest("No organisers found");
            }
        }
        /// <summary>
        /// Service to Check Email Is Valid or Not
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if valid else false</returns>
        [AllowAnonymous]
        [HttpGet("IsEmailTaken")]
        public async Task<IActionResult> IsEmailTaken(string email)
        {
            var result = await _organiserServices.IsOrganiserAvailableWithEmail(email);
            _fileLogger.AddInfoToFile("[IsEmailTaken] Check IsOrgansierAvailablewithEmail Success");

            return Ok(new
            {
                IsEmailTaken = result.IsOrganiserEmailAvailable,
                Message = result.Message
            });
        }
        /// <summary>
        /// Service to Get All Owners 
        /// </summary>
        /// <returns>List of all the Owners</returns>
        [HttpGet("AllOwners")]
        public async Task<IActionResult> GetAllOwners()
        {
            var result = await _organiserServices.GetAllOwners();
            if (result is not null)
            {
                _fileLogger.AddInfoToFile("[GetAllOwners] Fetch All Active Owners Success");
                return Ok(result);
            }
            else
            {
                _fileLogger.AddExceptionToFile("[GetAllOwners] Fetch All Active Owners Failed");
                return BadRequest("Error");
            }
        }


    }
}