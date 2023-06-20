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
        public async Task<IActionResult> AddNewEvent()
        {
            try
            {

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
                BLEvent bLEvent = new();
                bLEvent.EventName = Request.Form.First(e => e.Key == "EventName").Value;
                bLEvent.StartDate = DateTime.Parse(Request.Form.First(e => e.Key == "StartDate").Value);
                bLEvent.EndDate = DateTime.Parse(Request.Form.First(e => e.Key == "EndDate").Value);
                bLEvent.CategoryId = byte.Parse(Request.Form.First(e => e.Key == "CategoryId").Value);
                bLEvent.Capacity = int.Parse(Request.Form.First(e => e.Key == "Capacity").Value);
                bLEvent.AvailableSeats = int.Parse(Request.Form.First(e => e.Key == "AvailableSeats").Value);
                bLEvent.ProfileImgBody = bLEventImages[0].ImgBody;
                bLEvent.Description = Request.Form.First(e => e.Key == "Description").Value;
                bLEvent.Location = Request.Form.First(e => e.Key == "Location").Value;
                bLEvent.Country = Request.Form.First(e => e.Key == "Country").Value;
                bLEvent.State = Request.Form.First(e => e.Key == "State").Value;
                bLEvent.City = Request.Form.First(e => e.Key == "City").Value;
                bLEvent.IsPublished = bool.Parse(Request.Form.First(e => e.Key == "IsPublished").Value);
                bLEvent.IsCancelled = false;
                bLEvent.MaxNoOfTicketsPerTransaction = byte.Parse(Request.Form.First(e => e.Key == "MaxNoOfTicketsPerTransaction").Value);
                bLEvent.CreatedOn = DateTime.Parse(Request.Form.First(e => e.Key == "CreatedOn").Value);
                bLEvent.UpdatedOn = DateTime.Parse(Request.Form.First(e => e.Key == "UpdatedOn").Value);
                bLEvent.IsFree = bool.Parse(Request.Form.First(e => e.Key == "IsFree").Value);
                bLEvent.EventStartingPrice = int.Parse(Request.Form.First(e => e.Key == "EventStartingPrice").Value);
                bLEvent.EventEndingPrice = int.Parse(Request.Form.First(e => e.Key == "EventEndingPrice").Value);
                bLEvent.IsActive = true;
                bLEvent.OrganisationId = Guid.Parse(Request.Form.First(e => e.Key == "OrganisationId").Value);
                bLEvent.FormId = Guid.Parse(Request.Form.First(e => e.Key == "FormId").Value);
                bLEvent.RegistrationStatusId = byte.Parse(Request.Form.First(e => e.Key == "RegistrationStatusId").Value);
                bLEvent.CreatedBy = Guid.Parse(Request.Form.First(e => e.Key == "CreatedBy").Value);
                if(Request.Form.Where(e => e.Key == "AcceptedBy").ToList().Count() > 0)
                {
                    bLEvent.AcceptedBy = Guid.Parse(Request.Form.First(e => e.Key == "AcceptedBy").Value);
                }
                bLEventImages.RemoveAt(0);
                (BLEvent Event, string message) AddNewEvent = await _eventServices.Add(bLEvent,bLEventImages);
                if(AddNewEvent.Event != null) { return Ok(AddNewEvent.Event); }
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
        public async Task<IActionResult> GetAllActivePublishedEvents(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                int skipCount = (pageNumber - 1) * pageSize;
                List<BLEvent> paginatedEvents = await _eventServices.GetAllActivePublishedEvents(pageNumber, pageSize);
                if (paginatedEvents == null)
                {
                    return BadRequest("Error in BLL");
                }
                return Ok(paginatedEvents);
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

        //[HttpGet("GetAllActivePublishedEventsByMode")]
        //public async Task<IActionResult> GetAllActivePublishedEventsByMode(bool isOffline)
        //{
        //    try
        //    {
        //        List<BLEvent> AllEvents = await _eventServices.GetAllActivePublishedEventsByMode(isOffline);
        //        if (AllEvents == null) { return BadRequest("error in BL"); }
        //        return Ok(AllEvents);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

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

        [HttpGet("GetAllCreatedEventsByOrganisation")]
        public async Task<IActionResult> GetAllCreatedEventsByOrganisation(Guid orgId)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllCreatedEventsByOrganisation(orgId);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllCreatedEventsByOrganiser")]
        public async Task<IActionResult> GetAllCreatedEventsByOrganiser(Guid organiserId)
        {
            try
            {
                List<BLEvent> AllEvents = await _eventServices.GetAllCreatedEventsByOrganiser(organiserId);
                if (AllEvents == null) { return BadRequest("error in BL"); }
                return Ok(AllEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFilteredEvents")]
        public async Task<IActionResult> GetFilteredEvents(DateTime startDate, DateTime endDate, decimal startPrice, decimal endPrice, string location, bool isFree, List<int> categoryIds, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Set default values if parameters are not provided
                startDate = startDate == default ? DateTime.MinValue : startDate;
                endDate = endDate == default ? DateTime.MaxValue : endDate;
                startPrice = startPrice == default ? 0 : startPrice;
                endPrice = endPrice == default ? decimal.MaxValue : endPrice;
                location = location ?? string.Empty;
                categoryIds = categoryIds ?? new List<int>();

                var result = await _eventServices.GetFilteredEvents(startDate, endDate, startPrice, endPrice, location, isFree, categoryIds, pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
