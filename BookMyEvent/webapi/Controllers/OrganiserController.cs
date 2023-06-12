using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<(bool IsSuccessfull, string Message)> RegisterOrganiser((BLAdministrator profile, BLOrganisation organisation) organiser)
        {
            throw new NotImplementedException();
        }

    }
}
