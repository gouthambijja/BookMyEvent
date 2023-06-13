using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventServices _eventServices;
        public EventController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpPost("addevent")]
        public async Task<IActionResult> AddNewEvent([FromBody] AddEvent newEvent)
        {
            try
            {

                (BLEvent EventDetails, List<BLRegistrationFormFields> RegistrationFormFields, BLForm EventForm, List<BLEventImages> EventImages) new_Event;
                List<BLEventImages> bLEventImages = new List<BLEventImages>();
                var images = Request.Form.Files;
                if (images != null && images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        var memoryStream = new MemoryStream();
                        await image.CopyToAsync(memoryStream);
                        var imageBody = memoryStream.ToArray();
                        bLEventImages.Add(new BLEventImages()
                        {
                            ImgBody = imageBody,
                            ImgName = "CoverImage",
                            ImgType = "CoverImage"
                        });
                    }
                    bLEventImages[0].ImgType = "profile";
                    bLEventImages[0].ImgName = "profile";

                }
                new_Event.EventDetails = newEvent.EventDetails;
                new_Event.RegistrationFormFields = newEvent.RegistrationFormFields;
                new_Event.EventForm = newEvent.EventForm;
                new_Event.EventImages = bLEventImages;
                (BLEvent _NewEvent, string message) AddNewEvent = await _eventServices.Add(new_Event);
                if(AddNewEvent._NewEvent != null) { return Ok(AddNewEvent._NewEvent); }
                return BadRequest(AddNewEvent.message);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
