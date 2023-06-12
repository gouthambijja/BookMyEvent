using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class EventImageRepository:IEventImageRepository
    {
       private readonly EventManagementSystemTeamZealContext _context;

        public EventImageRepository(EventManagementSystemTeamZealContext context)
        {
            _context = context;
        }

        public async Task<List<EventImage>> GetImagesByEventId(Guid eventId)
        {
            try
            {
                List<EventImage> eventImages = await _context.EventImages.Where(i => i.EventId.Equals(eventId)).ToListAsync();
                if(eventImages != null)
                {
                    return eventImages;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddMultipleImages(List<EventImage> eventImages)
        {
            try
            {
                await _context.EventImages.AddRangeAsync(eventImages);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
