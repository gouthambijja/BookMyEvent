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
        /// An Asynchronous method that gets all the events that are active and published and are of a particular category
        /// </summary>
        /// <param name="categoryId">
        /// Takes the Id of the category as input as byte type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsByCategoryId(byte categoryId);

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
        /// An Asynchronous method that gets all the events that are active and published and starts from the date
        /// </summary>
        /// <param name="date">
        /// Takes the date of the event as input as DateTime type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsByStartDate(DateTime date);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and ends by the date
        /// </summary>
        /// <param name="date">
        /// Takes the date of the event as input as DateTime type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsByEndDate(DateTime date);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and starts from the start date and ends by the end date
        /// </summary>
        /// <param name="startDate">
        /// Takes the start date of the event as input as DateTime type
        /// </param>
        /// <param name="endDate">
        /// Takes the end date of the event as input as DateTime type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsByStartDateAndEndDate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and has starting price
        /// </summary>
        /// <param name="startingPrice">
        /// Takes the starting price of the event as input as decimal type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsHavingMoreThanPrice(decimal startingPrice);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and has ending price
        /// </summary>
        /// <param name="endingPrice">
        /// Takes the ending price of the event as input as decimal type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsHavingLessThanPrice(decimal endingPrice);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and has price in the range
        /// </summary>
        /// <param name="startingPrice">
        /// Takes the starting price of the event as input as decimal type
        /// </param>
        /// <param name="endingPrice">
        /// Takes the ending price of the event as input as decimal type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsHavingPriceRange(decimal startingPrice, decimal endingPrice);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and has name
        /// </summary>
        /// <param name="name">
        /// Takes the name of the event as input as string type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsHavingName(string name);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published based on the mode of event
        /// </summary>
        /// <param name="isOffline">
        /// Takes the mode of event as input as bool type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedEventsByMode(bool isOffline);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active and published and based on is free or not
        /// </summary>
        /// <param name="isFree">
        /// Takes the is free as input as bool type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActivePublishedIsFreeEvents(bool isFree);

        /// <summary>
        /// An Asynchronous method that gets all the events that are active based on is published or not
        /// </summary>
        /// <param name="isPublished">
        /// Takes the is published as input as bool type
        /// </param>
        /// <returns>
        /// Returns a list of Event objects
        /// </returns>
        Task<List<Event>> GetAllActiveEventsByIsPublished(bool isPublished);


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
        /// <returns>
        /// Returns a Event object
        /// </returns>
        Task<Event> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt);

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
    }
}
