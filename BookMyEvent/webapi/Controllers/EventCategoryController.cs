using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventCategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public EventCategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        /// <summary>
        /// Service to Get All the EventCategories
        /// </summary>
        /// <returns>List Of Event Categories</returns>
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
        /// <summary>
        /// Method to add new event category 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(BLEventCategory? category)
        {
            try
            {
                var _category = await _categoryServices.AddEventCategory(category);
                if (_category != null)
                {
                    return Ok(_category);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }   
        /// <summary>
        /// Method to update event Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(BLEventCategory? category)
        {
            try
            {
                var _category = await _categoryServices.UpdateEventCategory(category);
                if (_category != null) return Ok(_category);
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
