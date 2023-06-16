using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetOrganisations()
        {
            var result = await _organisationServices.GetAllOrganisations();
            Console.WriteLine(result.Count());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganisationById(Guid id)
        {
            var result = await _organisationServices.GetOrganisationById(id);
            return Ok(result);
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
            return Ok(new {IsTaken = result});
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
