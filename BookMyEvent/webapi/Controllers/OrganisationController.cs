using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Peer,Secondary_Owner,Admin")]
    public class OrganisationController : ControllerBase
    {
        private readonly IOrganisationServices _organisationServices;
        public OrganisationController(IOrganisationServices organisationServices)
        {
            _organisationServices = organisationServices;
        }
        /// <summary>
        /// service to Get All the Organizations
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>List of Organizations</returns>
        [HttpGet]
        public async Task<IActionResult> GetOrganisations(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _organisationServices.GetAllOrganisations(pageNumber, pageSize);
            //Console.WriteLine(result.Count());
            return Ok(new {organisations = result.bLOrganisations, total = result.totalBLOrganisations});
        }
        /// <summary>
        /// Service to GEt a single Organization
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Organization Details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganisationById(Guid id)
        {
            var result = await _organisationServices.GetOrganisationById(id);
            if(result is not null)
                return Ok(result);
            else return NotFound("Organisation not found");
        }
        /// <summary>
        /// Service to Update Organization
        /// </summary>
        /// <param name="organisation"></param>
        /// <returns>Updated Organization Details</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganisation(BLOrganisation organisation)
        {
            var result = await _organisationServices.UpdateOrganisation(organisation);
            if(result is not null)
                return Ok(result);
            else return BadRequest("Organisation not updated");
        }
        /// <summary>
        /// Service to Check OrganizationName is Valid Or Not
        /// </summary>
        /// <param name="orgName"></param>
        /// <returns>returns true if valid else false</returns>
        [AllowAnonymous]
        [HttpGet("CheckOrganisationName/{orgName}")]
        public async Task<IActionResult> CheckOrganisationName(string orgName)
        {
            var result = await _organisationServices.IsOrganisationNameTaken(orgName);
            return Ok(result);
        }
        /// <summary>
        /// Service to Get Organization by Name
        /// </summary>
        /// <param name="orgName"></param>
        /// <returns>A tuple conatins of bool type and Id of Organization</returns>
        [AllowAnonymous]
        [HttpGet("getOrgIdByName/{orgName}")]
        public async Task<IActionResult> GetOrgIdByName(string orgName)
        {
            try
            {
                var orgId = await _organisationServices.GetOrgIdByName(orgName);
                if(orgId is not null)
                {
                    return Ok(new
                    {
                        orgId=orgId,
                        isExists=true
                    });
                }
                else
                {
                    return Ok(new
                    {
                        orgId = "",
                        isExists = false

                    });
                }
               
            }
            catch
            {
                return BadRequest("error");
            }
        }
        /// <summary>
        /// Service to Delete Organization
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted Organization Details</returns>
        [HttpDelete("{orgId}/BlockedBy/{blockerId}")]
        public async Task<IActionResult> DeleteOrganisation(Guid orgId, Guid blockerId)
        {
            var result = await _organisationServices.BlockOrganisation(orgId, blockerId);
            if (result.IsOrganisationBlockToggled)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }
    }
}
