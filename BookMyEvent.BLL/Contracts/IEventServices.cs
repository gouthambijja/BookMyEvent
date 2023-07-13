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
        Task<(BLEvent _NewEvent, string message)> Add(BLEvent EventDetails, List<BLEventImages> EventImages);

        /// <summary>
        /// An asynchronous method to get an event by its Id
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns>
        /// Returns the BLEvent object
        /// </returns>
        Task<BLEvent> GetEventById(Guid EventId);

        /// <summary>
        /// An asynchronous method to update an event
        /// </summary>
        /// <param name="_event"></param>
        /// <returns>
        /// Returns a tuple of updated BLEvent and string
        /// </returns>
        Task<(BLEvent, string Message)> UpdateEvent(BLEvent _event);

        /// <summary>
        /// An asynchronous method to delete an event
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Returns a tuple of bool to say if event is deleted succeefully or not and string
        /// </returns>
        Task<(bool, string Message)> DeleteEvent(Guid id);

        /// <summary>
        /// An asynchronous method to get all active events
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetAllActivePublishedEvents(int pageNumber, int pageSize);

        /// <summary>
        /// An asynchronous method to get all active events by organisation
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetAllActivePublishedEventsByOrgId(Guid orgId);

        /// <summary>
        /// An asynchronous method to get all active events by location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetAllActivePublishedEventsByLocation(string location);

        /// <summary>
        /// An asynchronous method to update the registration status of an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="registrationStatusId"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedAt"></param>
        /// <returns>
        /// Returns the updated BLEvent object
        /// </returns>
        Task<BLEvent> UpdateEventRegistrationStatus(Guid eventId, byte registrationStatusId, Guid updatedBy, DateTime updatedAt);

        /// <summary>
        /// An asynchronous method to cancel an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedAt"></param>
        /// <returns>
        /// Returns the updated BLEvent object
        /// </returns>
        Task<BLEvent> UpdateIsCancelledEvent(Guid eventId, Guid updatedBy, DateTime updatedAt);

        /// <summary>
        /// An asynchronous method to publish an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedAt"></param>
        /// <returns>
        /// Returns the updated BLEvent object
        /// </returns>
        Task<BLEvent> UpdateIsPublishedEvent(Guid eventId, Guid updatedBy, DateTime updatedAt);

        /// <summary>
        /// An asynchronous method to accept an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="acceptBy"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedAt"></param>
        /// <returns>
        /// Returns the updated BLEvent object
        /// </returns>
        Task<BLEvent> UpdateAcceptedBy(Guid eventId, Guid acceptBy, Guid updatedBy, DateTime updatedAt);

        /// <summary>
        /// An asynchronous method to reject an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="rejectedBy"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedAt"></param>
        /// <param name="reason"></param>
        /// <returns>
        /// Returns the updated BLEvent object
        /// </returns>
        Task<BLEvent> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt, string reason);

        /// <summary>
        /// An asynchronous method to get all events by organisation
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetAllCreatedEventsByOrganisation(Guid orgId);

        /// <summary>
        /// An asynchronous method to get all events by organiser
        /// </summary>
        /// <param name="organiserId"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetAllCreatedEventsByOrganiser(Guid organiserId);


        // <summary>
        /// An asynchronous method to softDelete the event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedOn"></param>
        /// <returns>
        /// Returns a tuple of bool to say if event is deleted succeefully or not and string
        /// </returns>
        Task<(bool isActiveUpdated, string message)> SoftDelete(Guid eventId, Guid updatedBy, DateTime updatedOn);

        /// <summary>
        /// An asynchronous method to get all events by filtering
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startPrice"></param>
        /// <param name="endPrice"></param>
        /// <param name="location"></param>
        /// <param name="name"></param>
        /// <param name="isFree"></param>
        /// <param name="categoryIds"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetFilteredEvents(DateTime? startDate, DateTime? endDate, decimal? startPrice, decimal? endPrice, string? location,string? name, bool? isFree, List<int>? categoryIds, int pageNumber, int? pageSize);
        
        /// <summary>
        /// An asynchronous method to get all event images
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>
        /// Returns a list of BLEventImages
        /// </returns>
        Task<List<BLEventImages>> GetEventImages(Guid eventId);

        /// <summary>
        /// An asynchronous method to get all past events of organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// Retuns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetAllPastEventsByOrganisationId(Guid organisationId, int pageNumber, int pageSize);

        /// <summary>
        /// An asynchronous method to get all past events of organiser
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetAllPastEventsByOrganiserId(Guid organisationId, int pageNumber, int pageSize);

        /// <summary>
        /// An asynchronous method to get all event requests of organiser
        /// </summary>
        /// <param name="organiserId"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetOrganiserRequestedEvents(Guid organiserId);

        /// <summary>
        /// An asynchronous method to get all event requests of organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <returns>
        /// Returns a list of BLEvent
        /// </returns>
        Task<List<BLEvent>> GetOrganisationRequestedEvents(Guid organisationId);

        /// <summary>
        /// An asynchronous method to get number of event requests of organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <returns>
        /// Returns an integer
        /// </returns>
        Task<int> GetNoOfOrganisationRequestedEvents(Guid organisationId);

        /// <summary>
        /// An asynchronous method to update event available seats
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="availableSeats"></param>
        /// <returns>
        /// Returns a bool to say if event available seats is updated or not
        /// </returns>
        Task<bool> UpdateEventAvailableSeats(Guid eventId, int availableSeats);

        /// <summary>
        /// An asynchronous method to get number of past events of organiser and organisation
        /// </summary>
        /// <param name="organiserId"></param>
        /// <param name="organisationId"></param>
        /// <returns>
        /// Returns a tuple of integers of number of past events of organiser and organisation
        /// </returns>

        Task<(int NoOfOrganiserPastEvents, int NoOfOrganisationPastEvents)> GetNoOfPastEvents(Guid organiserId, Guid organisationId);

    }
}
