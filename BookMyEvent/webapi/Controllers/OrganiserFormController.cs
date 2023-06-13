using BookMyEvent.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserFormController : ControllerBase
    {
        private readonly IOrganiserFormServices _organiserFormServices;
        public OrganiserFormController(IOrganiserFormServices organiserFormServices)
        {
            _organiserFormServices = organiserFormServices;
        }

        
    }
}
