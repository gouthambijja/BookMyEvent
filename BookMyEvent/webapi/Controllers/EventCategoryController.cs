using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventCategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        private readonly FileLogger _fileLogger;
        public EventCategoryController(ICategoryServices categoryServices, FileLogger fileLogger)
        {
            _categoryServices = categoryServices;
            _fileLogger = fileLogger;
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
                _fileLogger.AddInfoToFile("[Get] Fetches all the event Categories Success");
                return Ok(await _categoryServices.GetAllEventCategories());
            }
            catch
            {
                _fileLogger.AddExceptionToFile("[Get] Fetches all the event Categories Failure");
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
                    _fileLogger.AddInfoToFile("[Post] Adds the event Category Success");
                    return Ok(_category);
                }
                _fileLogger.AddExceptionToFile("[Post] Post event Category Failure");
                return BadRequest("error");
            }
            catch
            {
                _fileLogger.AddExceptionToFile("[Post] Fetches all the event Category Failure in try catch");
                return BadRequest("error");
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
                _fileLogger.AddInfoToFile("[Put] Updates the event Category Success");
                return BadRequest("error");
            }
            catch
            {
                _fileLogger.AddExceptionToFile("[Put] Updates the event Category Failure");
                return BadRequest("error");
            }
        }
    }
}
