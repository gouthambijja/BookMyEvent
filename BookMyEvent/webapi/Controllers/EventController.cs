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
        [HttpGet("geteventbyid")]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            try
            {
                BLEvent _event = await _eventServices.GetEventById(eventId);
                if( _event == null ) { return BadRequest("error in BL"); }
                return Ok(_event);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateevent")]
        public async Task<IActionResult> UpdateEvent([FromBody] BLEvent newEvent)
        {
            try
            {
                (BLEvent _event, string message) updatedEvent = await _eventServices.UpdateEvent(newEvent);
                if(updatedEvent._event==null ) { return BadRequest(updatedEvent.message); }
                return Ok(updatedEvent._event);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deleteeventpermanently")]
        public async Task<IActionResult> DeleteEventPermanently(Guid eventId)
        {
            try
            {
                (bool isDeleted, string message) deleteEvent = await _eventServices.DeleteEvent(eventId);
                if (!deleteEvent.isDeleted) { return BadRequest(deleteEvent.message); }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEvents")]
        public async Task<IActionResult> GetAllActivePublishedEvents()
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEvents();
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsByCategoryId")]
        public async Task<IActionResult> GetAllActivePublishedEventsByCategoryId(byte categoryId)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByCategoryId(categoryId);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsByOrgId")]
        public async Task<IActionResult> GetAllActivePublishedEventsByOrgId(Guid orgId)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByOrgId(orgId);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsByLocation")]
        public async Task<IActionResult> GetAllActivePublishedEventsByLocation(string location)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByLocation(location);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsByStartDate")]
        public async Task<IActionResult> GetAllActivePublishedEventsByStartDate(DateTime date)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByStartDate(date);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsByEndDate")]
        public async Task<IActionResult> GetAllActivePublishedEventsByEndDate(DateTime date)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByEndDate(date);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsByStartDateAndEndDate")]
        public async Task<IActionResult> GetAllActivePublishedEventsByStartDateAndEndDate(DateTime startDate,DateTime endDate)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByStartDateAndEndDate(startDate,endDate);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsHavingMoreThanPrice")]
        public async Task<IActionResult> GetAllActivePublishedEventsHavingMoreThanPrice(decimal startingPrice)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsHavingMoreThanPrice(startingPrice);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsHavingLessThanPrice")]
        public async Task<IActionResult> GetAllActivePublishedEventsHavingLessThanPrice(decimal endingPrice)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsHavingMoreThanPrice(endingPrice);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsHavingPriceRange")]
        public async Task<IActionResult> GetAllActivePublishedEventsHavingPriceRange(decimal startingPrice, decimal endingPrice)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsHavingPriceRange(startingPrice, endingPrice);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsHavingName")]
        public async Task<IActionResult> GetAllActivePublishedEventsHavingName(string name)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsHavingName(name);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedEventsByMode")]
        public async Task<IActionResult> GetAllActivePublishedEventsByMode(bool isOffline)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByMode(isOffline);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllActivePublishedIsFreeEvents")]
        public async Task<IActionResult> GetAllActivePublishedIsFreeEvents(bool isFree)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedIsFreeEvents(isFree);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(" GetAllActiveEventsByIsPublished")]
        public async Task<IActionResult> GetAllActiveEventsByIsPublished(bool isFree)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedIsFreeEvents(isFree);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(" UpdateEventRegistrationStatus")]
        public async Task<IActionResult> UpdateEventRegistrationStatus(Guid eventId, byte registrationStatusId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                BLEvent Events = await _eventServices.UpdateEventRegistrationStatus(eventId,registrationStatusId,updatedBy,updatedAt);
                if (Events == null) { return BadRequest("error in BL"); }
                return Ok(Events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(" UpdateIsCancelledEvent")]
        public async Task<IActionResult> UpdateIsCancelledEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                BLEvent Events = await _eventServices.UpdateIsCancelledEvent(eventId,  updatedBy, updatedAt);
                if (Events == null) { return BadRequest("error in BL"); }
                return Ok(Events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(" UpdateIsPublishedEvent")]
        public async Task<IActionResult> UpdateIsPublishedEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                BLEvent Events = await _eventServices.UpdateIsPublishedEvent(eventId, updatedBy, updatedAt);
                if (Events == null) { return BadRequest("error in BL"); }
                return Ok(Events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(" UpdateAcceptedBy")]
        public async Task<IActionResult> UpdateAcceptedBy(Guid eventId, Guid acceptBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                BLEvent Events = await _eventServices.UpdateAcceptedBy(eventId, acceptBy, updatedBy, updatedAt);
                if (Events == null) { return BadRequest("error in BL"); }
                return Ok(Events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(" UpdateRejectedBy")]
        public async Task<IActionResult> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                BLEvent Events = await _eventServices.UpdateRejectedBy(eventId,rejectedBy, updatedBy, updatedAt);
                if (Events == null) { return BadRequest("error in BL"); }
                return Ok(Events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
