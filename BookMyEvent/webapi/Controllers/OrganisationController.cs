using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly IOrganisationServices _organisationServices;
        public OrganisationController(IOrganisationServices organisationServices)
        {
            _organisationServices = organisationServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrganisations(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _organisationServices.GetAllOrganisations(pageNumber, pageSize);
            //Console.WriteLine(result.Count());
            return Ok(new {organisations = result.bLOrganisations, total = result.totalBLOrganisations});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganisationById(Guid id)
        {
            var result = await _organisationServices.GetOrganisationById(id);
            if(result is not null)
                return Ok(result);
            else return NotFound("Organisation not found");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganisation(BLOrganisation organisation)
        {
            var result = await _organisationServices.UpdateOrganisation(organisation);
            if(result is not null)
                return Ok(result);
            else return BadRequest("Organisation not updated");
        }

        [HttpGet("CheckOrganisationName/{orgName}")]
        public async Task<IActionResult> CheckOrganisationName(string orgName)
        {
            var result = await _organisationServices.IsOrganisationNameTaken(orgName);
            return Ok(result);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganisation(Guid id)
        {
            var result = await _organisationServices.BlockOrganisation(id);
            if (result.IsOrganisationBlockToggled)
                return Ok(result.Message);
            else return BadRequest(result.Message);
        }
    }
}
