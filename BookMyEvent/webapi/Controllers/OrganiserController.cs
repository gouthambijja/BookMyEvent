using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserController : ControllerBase
    {
        private readonly IOrganiserServices _organiserServices;
        public OrganiserController(IOrganiserServices organiserServices)
        {
            _organiserServices = organiserServices;
        }
        [HttpPost("RegisterOrganiser")]
        public async Task<IActionResult> RegisterOrganiserOwner(Tuple<BLAdministrator, BLOrganisation> organiser)
        {
            //BLAdministrator profile, BLOrganisation organisation
            //var result = await _organiserServices.RegisterOwner(organiser.profile, organiser.organisation);
            BLAdministrator administrator = organiser.Item1;
            BLOrganisation organisation = organiser.Item2;
            var result = await _organiserServices.RegisterOwner(administrator, organisation);
            if(result.IsSuccessfull)
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


    }
}