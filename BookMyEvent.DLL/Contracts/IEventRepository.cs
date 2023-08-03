using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    public interface IEventRepository
    {

        /// <summary>
        /// An Asynchronous method that adds a new event to the database
        /// </summary>
        /// <param name="_event">
        /// Takes the Event object as input
        /// </param>
        /// <returns>
        /// Returns a tuple of bool to say if Event is added successfully or not and a string message
        /// </returns>
        Task<Event> AddEvent(Event _event);

        /// <summary>
        /// An Asynchronous method that gets a event by its Id
        /// </summary>
        /// <param name="Id">
        /// Takes the Id of the event as input as Guid type
        /// </param>
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<Event> GetEventById(Guid Id);

        /// <summary>
        /// An Asynchronous method that gets a event by its Name
        /// </summary>
        /// <param name="_event">
        /// Takes the Name of the event as input as string type
        /// </param>
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<(Event, string Message)> UpdateEvent(Event _event);

        /// <summary>
        /// An Asynchronous method that deletes a event
        /// </summary>
        /// <param name="id">
        /// Takes the Id of the event as input as Guid type
        /// </param>
        /// <returns>
        /// Returns a tuple of bool to say if Event is deleted successfully or not and a string message
        /// </returns>
        Task<(bool, string Message)> DeleteEvent(Guid id);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published
        /// </summary>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEvents(int pageNumber, int pageSize);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and created by particular organization
        /// </summary>
        /// <param name="orgId">
        /// Takes the Id of the Organization as input as Guid type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsByOrgId(Guid orgId);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and in the location
        /// </summary>
        /// <param name="location">
        /// Takes the location as input as string type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsByLocation(string location);

        /// <summary>
        /// An asynchronous mthod to update the event registration status
        /// </summary>
        /// <param name="eventId">
        /// Takes the event id as input as Guid type
        /// </param>
        /// <param name="registrationStatusId">
        /// Takes the registration status id as input as byte type
        /// </param>
        /// <param name="updatedBy">
        /// Takes the updated by as input as Guid type
        /// </param>
        /// <param name="updatedAt">
        /// Takes the updated at as input as DateTime type
        /// </param>
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<Event> UpdateEventRegistrationStatus(Guid eventId,  byte registrationStatusId, Guid updatedBy, DateTime updatedAt);

        /// <summary>
        /// An asynchronous mthod to update the event is cancelled status
        /// </summary>
        /// <param name="eventId">
        /// Takes the event id as input as Guid type
        /// </param>
        /// <param name="updatedBy">
        /// Takes the updated by as input as Guid type
        /// </param>
        /// <param name="updatedAt">
        /// Takes the updated at as input as DateTime type
        /// </param>
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<Event> UpdateIsCancelledEvent(Guid eventId, Guid updatedBy, DateTime updatedAt);

        /// <summary>
        /// An asynchronous mthod to update the event is published status
        /// </summary>
        /// <param name="eventId">
        /// Takes the event id as input as Guid type
        /// </param>
        /// <param name="updatedBy">
        /// Takes the updated by as input as Guid type
        /// </param>
        /// <param name="updatedAt">
        /// Takes the updated at as input as DateTime type
        /// </param>
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<Event> UpdateIsPublishedEvent(Guid eventId, Guid updatedBy, DateTime updatedAt);

        /// <summary>
        /// An asynchronous mthod to update the event is Accepted status
        /// </summary>
        /// <param name="eventId">
        /// Takes the event id as input as Guid type
        /// </param>
        /// <param name="acceptBy">
        /// Takes the accepted by as input as Guid type
        /// </param>
        /// <param name="updatedBy">
        /// Takes the updated by as input as Guid type
        /// </param>
        /// <param name="updatedAt">
        /// Takes the updated at as input as DateTime type
        /// </param>
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<Event> UpdateAcceptedBy(Guid eventId, Guid acceptBy, Guid updatedBy, DateTime updatedAt);


        /// <summary>
        /// An asynchronous mthod to update the event is Rejected status
        /// </summary>
        /// <param name="eventId">
        /// Takes the event id as input as Guid type
        /// </param>
        /// <param name="rejectedBy">
        /// Takes the rejected by as input as Guid type
        /// </param>
        /// <param name="updatedBy">
        /// Takes the updated by as input as Guid type
        /// </param>
        /// <param name="updatedAt">
        /// Takes the updated at as input as DateTime type
        /// </param>
        /// <param name="reason">
        /// Takes the reason as input as string type
        /// </param>
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<Event> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt, string reason);

        /// <summary>
        /// An asynchronous mthod to get list of all the events created by an organisation
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllCreatedEventsByOrganisation(Guid orgId);

        /// <summary>
        /// An asynchronous mthod to get list of all the events created by an organiser
        /// </summary>
        /// <param name="organiserId"></param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllCreatedEventsByOrganiser(Guid organiserId);

        /// <summary>
        /// An asynchronous mthod to get list of all the events according to the filters
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startPrice"></param>
        /// <param name="endPrice"></param>
        /// <param name="location"></param>
        /// <param name="isFree"></param>
        /// <param name="categoryIds"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetFilteredEvents(DateTime? startDate, DateTime? endDate, decimal? startPrice, decimal? endPrice, string? location,string name, bool? isFree, List<int>? categoryIds, int pageNumber, int? pageSize);

        /// <summary>
        /// An asynchronous method to update the IsActive status of an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedOn"></param>
        /// <returns>
        /// Returns a tuple of bool to say if event is active is updated succeefully or not and string
        /// </returns>
        Task<(bool isActiveUpdated, string message)> UpdateIsActive(Guid eventId, Guid updatedBy, DateTime updatedOn);



        /// <summary>
        /// An asynchronous method to get the list of past event by an organiser
        /// </summary>
        /// <param name="organiserId"></param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetPastEventsByOrganiser(Guid organiserId, int pageNumber, int pageSize);

        /// <summary>
        /// An asynchronous method to get the list of past events by an organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetPastEventsByOrganisation(Guid organisationId, int pageNumber, int pageSize);

        /// <summary>
        /// An asynchronous method to get the list of requested events by an organiser
        /// </summary>
        /// <param name="organiserId"></param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetOrganiserRequestedEvents(Guid organiserId);

        /// <summary>
        /// An asynchronous method to get the list of requested events by an organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetOrganisationRequestedEvents(Guid organisationId);

        /// <summary>
        /// An asynchronous method to get the number of present active event requests in an organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <returns>
        /// Returns an integer
        /// </returns>
        Task<int> GetNoOfOrganisationRequestedEvents(Guid organisationId);
        /// <summary>
        /// return all the events whose ids present in the eventIds list
        /// </summary>
        /// <param name="eventIds"></param>
        /// <returns>returns a list of events</returns>
        Task<List<Event>> GetEventsByEventIds(List<Guid> eventIds);
        /// <summary>
        /// Updates the available seats Property of the Event class
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="availableSeats"></param>
        /// <returns>
        /// Returns a boolean saying if the available seats is updated or not
        /// </returns>
        Task<bool> UpdateEventAvailableSeats(Guid eventId,int availableSeats);

        /// <summary>
        /// An asynchronous method to get the number of past event of an organiser
        /// </summary>
        /// <param name="organiserId"></param>
        /// <returns>
        /// Returns an integer
        /// </returns>
        Task<int> GetOrganiserTotalNoOfPastEvents(Guid organiserId);

        /// <summary>
        /// An asynchronous method to get the number of past event of an organisation
        /// </summary>
        /// <param name="organisationId"></param>
        /// <returns>
        /// Returns an integer
        /// </returns>
        Task<int> GetOrganisationTotalNoOfPastEvents(Guid organisationId);
    }
}
