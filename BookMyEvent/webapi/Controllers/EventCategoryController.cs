using BookMyEvent.BLL.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventCategoryController:ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public EventCategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _categoryServices.GetAllEventCategories());
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
