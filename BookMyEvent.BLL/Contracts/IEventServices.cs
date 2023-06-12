using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface IEventServices
    {
        /// <summary>
        /// This method is to add a New Event by an Event Organiser
        /// This method is responsible for adding Event Details,Event Images,Event Forms and Registration Form Fields(Dynamic Form)
        /// </summary>
        /// <param name="NewEvent"></param>
        /// <returns> New Event </returns>
        Task<(BLEvent _NewEvent, string message)> Add((BLEvent EventDetails, List<BLRegistrationFormFields> RegistrationFormFields, BLForm EventForm, List<BLEventImages> EventImages) NewEvent);

        Task<BLEvent> GetEventById(Guid EventId);
        Task<(BLEvent, string Message)> UpdateEvent(BLEvent _event);
        Task<(bool, string Message)> DeleteEvent(Guid id);
        Task<List<BLEvent>> GetAllActivePublishedEvents();
        Task<List<BLEvent>> GetAllActivePublishedEventsByCategoryId(byte categoryId);
        Task<List<BLEvent>> GetAllActivePublishedEventsByOrgId(Guid orgId);
        Task<List<BLEvent>> GetAllActivePublishedEventsByLocation(string location);
        Task<List<BLEvent>> GetAllActivePublishedEventsByStartDate(DateTime date);
        Task<List<BLEvent>> GetAllActivePublishedEventsByEndDate(DateTime date);
        Task<List<BLEvent>> GetAllActivePublishedEventsByStartDateAndEndDate(DateTime startDate, DateTime endDate);
        Task<List<BLEvent>> GetAllActivePublishedEventsHavingMoreThanPrice(decimal startingPrice);
        Task<List<BLEvent>> GetAllActivePublishedEventsHavingLessThanPrice(decimal endingPrice);
        Task<List<BLEvent>> GetAllActivePublishedEventsHavingPriceRange(decimal startingPrice, decimal endingPrice);
        Task<List<BLEvent>> GetAllActivePublishedEventsHavingName(string name);
        Task<List<BLEvent>> GetAllActivePublishedEventsByMode(bool isOffline);
        Task<List<BLEvent>> GetAllActivePublishedIsFreeEvents(bool isFree);
        Task<List<BLEvent>> GetAllActiveEventsByIsPublished(bool isPublished);
        Task<BLEvent> UpdateEventRegistrationStatus(Guid eventId, byte registrationStatusId, Guid updatedBy, DateTime updatedAt);
        Task<BLEvent> UpdateIsCancelledEvent(Guid eventId, Guid updatedBy, DateTime updatedAt);
        Task<BLEvent> UpdateIsPublishedEvent(Guid eventId, Guid updatedBy, DateTime updatedAt);
        Task<BLEvent> UpdateAcceptedBy(Guid eventId, Guid acceptBy, Guid updatedBy, DateTime updatedAt);
        Task<BLEvent> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt);
    }
}
