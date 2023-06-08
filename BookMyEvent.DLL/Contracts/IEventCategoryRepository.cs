using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    public interface IEventCategoryRepository
    {
        /// <summary>
        /// An Asynchronous method that adds a new event category to the database
        /// </summary>
        /// <param name="_eventCategory">
        /// Takes the EventCategory object as input
        /// </param>
        /// <returns>
        /// Returns a tuple of bool to say if EventCategory is added successfully or not and a string message
        /// </returns>
        Task<(bool IsEventCategoryAdded, string Message)> AddEventCategory(EventCategory _eventCategory);

        /// <summary>
        /// An Asynchronous method that gets an event category by its Id
        /// </summary>
        /// <param name="Id">
        /// Takes the Id of the event category as input as byte type
        /// </param>
        /// <returns>
        /// Returns an EventCategory object 
        /// </returns>
        Task<EventCategory> GetEventCategoryById(byte Id);

        /// <summary>
        /// An Asynchronous method that updates an event category
        /// </summary>
        /// <param name="_eventCategory">
        /// Takes the EventCategory object as input
        /// </param>
        /// <returns>
        /// Returns a tuple of EventCategory object and a string message
        /// </returns>
        Task<(EventCategory, string Message)> UpdateEventCategory(EventCategory _eventCategory);

        /// <summary>
        /// An Asynchronous method that deletes an event category
        /// </summary>
        /// <param name="id">
        /// Takes the Id of the event category as input as byte type
        /// </param>
        /// <returns>
        /// Returns a tuples of bool to say if EventCategory is deleted successfully or not and a string message
        /// </returns>
        Task<(bool IsEventCategoryDeleted, string Message)> DeleteEventCategory(byte id);

        /// <summary>
        /// An Asynchronous method that gets all event categories
        /// </summary>
        /// <returns>
        /// Returns a list of EventCategory objects
        /// </returns>
        Task<List<EventCategory>> GetAllEventCategories();

    }
}
