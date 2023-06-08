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
    public class EventCategoryRepository : IEventCategoryRepository
    {
        EventManagementSystemTeamZealContext context;
        public EventCategoryRepository(EventManagementSystemTeamZealContext context)
        {
            this.context = context;
        }
        public async Task<(bool IsEventCategoryAdded, string Message)> AddEventCategory(EventCategory _eventCategory)
        {
            try
            {
                if (_eventCategory != null)
                {
                    await context.EventCategories.AddAsync(_eventCategory);
                    await context.SaveChangesAsync();
                    return (true, "Added");
                }
                else
                {
                    return (false, "Not Added");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<EventCategory> GetEventCategoryById(byte Id)
        {
            if (!Id.Equals(string.Empty))
            {
                return await context.EventCategories.FindAsync(Id);
                
            }
            else
            {
                return new EventCategory();
            }
        }
        public async Task<(EventCategory, string Message)> UpdateEventCategory(EventCategory _eventCategory)
        {
            var EventCategory = await context.EventCategories.FindAsync(_eventCategory.CategoryId);
            if (EventCategory != null)
            {
                EventCategory.CategoryName = _eventCategory.CategoryName;
                await context.SaveChangesAsync();
                await context.Entry(_eventCategory).GetDatabaseValuesAsync();
                return (_eventCategory, "EventCategory Added");
            }
            else
            {
                return (new EventCategory(), "EventCategory Not Added");
            }
        }
        public async Task<(bool IsEventCategoryDeleted, string Message)> DeleteEventCategory(byte id)
        {
            try
            {
                var EventCategory = await context.EventCategories.FindAsync(id);
                if (EventCategory != null)
                {
                    context.EventCategories.Remove(EventCategory);
                    await context.SaveChangesAsync();
                    return (true, "EventCategory Deleted Successfully");
                }
                else
                {
                    return (false, "EventCategory Not Found");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<List<EventCategory>> GetAllEventCategories()
        {
            try
            {
                return await context.EventCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<EventCategory>();
            }
        }
    }
}
