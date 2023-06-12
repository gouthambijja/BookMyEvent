using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    public interface IEventImageRepository
    {
        /// <summary>
        /// Method to get All Images of an Event
        /// </summary>
        /// <param name="eventId"> Event Id </param>
        /// <returns> List of all Images of an Event </returns>
        Task<List<EventImage>> GetImagesByEventId(Guid eventId);
        
       /// <summary>
       /// Method to add Multiple Images 
       /// </summary>
       /// <param name="eventImages"> List of Images to add </param>
       /// <returns>
       /// True:if Add operation succeeds
       /// False:if Add operation fails or in case of exception
       /// </returns>
        Task<bool> AddMultipleImages(List<EventImage> eventImages);
    }
}
