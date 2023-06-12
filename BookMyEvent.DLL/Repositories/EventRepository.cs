using BookMyEvent.DLL.Contracts;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookMyEvent.DLL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventManagementSystemTeamZealContext _db;
        public EventRepository(EventManagementSystemTeamZealContext db)
        {
            _db = db;
        }
        public async Task<Event> AddEvent(Event _event)
        {
            try
            {
                _db.Events.Add(_event);
                await _db.SaveChangesAsync();
                await _db.Entry(_event).GetDatabaseValuesAsync();
                return _event;
            }
            catch (Exception ex) { return null; }
        }

        public async Task<(bool, string Message)> DeleteEvent(Guid id)
        {
            try
            {
                var _event = _db.Events.Where(x => x.EventId == id).FirstOrDefault();
                if (_event != null)
                {
                    _db.Events.Remove(_event);
                    _db.SaveChanges();
                    return (true, "Event Deleted Successfully");
                }
                else
                {
                    return (false, "Event Not Found");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEvents()
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsByCategoryId(byte categoryId)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.CategoryId == categoryId).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }


        public async Task<List<Event>> GetAllActivePublishedEventsByEndDate(DateTime date)
        {

            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.EndDate <= date).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActiveEventsByIsPublished(bool isPublished)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsByLocation(string location)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.Location == location).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsByMode(bool isOffline)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.IsOffline == isOffline).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsByStartDate(DateTime date)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.StartDate >= date).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsByStartDateAndEndDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.StartDate >= startDate && x.EndDate <= endDate).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsByOrgId(Guid orgId)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.OrganisationId == orgId).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingLessThanPrice(decimal endingPrice)
        {

            //    try
            //    {
            //        var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.StartingPrice >= startingPrice && x.EndingPrice<=endingPrice).ToList();
            //        return events;
            //    }
            //    catch (Exception ex)
            //    {
            //        return new List<Event>();
            //    }
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingMoreThanPrice(decimal startingPrice)
        {

            //    try
            //    {
            //        var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.StartingPrice >= startingPrice && x.EndingPrice<=endingPrice).ToList();
            //        return events;
            //    }
            //    catch (Exception ex)
            //    {
            //        return new List<Event>();
            //    }
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingName(string name)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.EventName.Contains(name)).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingPriceRange(decimal startingPrice, decimal endingPrice)
        {
            //    try
            //    {
            //        var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.StartingPrice >= startingPrice && x.EndingPrice<=endingPrice).ToList();
            //        return events;
            //    }
            //    catch (Exception ex)
            //    {
            //        return new List<Event>();
            //    }
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetAllActivePublishedIsFreeEvents(bool isFree)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.IsFree == isFree).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<Event> GetEventById(Guid Id)
        {
            try
            {
                return await _db.Events.FindAsync(Id);
            }
            catch
            {
                return new Event();
            }
        }

        public async Task<Event> UpdateAcceptedBy(Guid eventId, Guid acceptBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                var _event = await _db.Events.FindAsync(eventId);
                if (_event != null)
                {
                    _event.AcceptedBy = acceptBy;
                    //_event.UpdatedBy = updatedBy;
                    //_event.updatedAt = updatedAt;
                    await _db.SaveChangesAsync();
                    await _db.Entry(_event).ReloadAsync();
                    return _event;
                }
                else
                {
                    return new Event();
                }
            }
            catch (Exception ex)
            {
                return new Event();

            }
        }

        public async Task<(Event, string Message)> UpdateEvent(Event _event)
        {
            try
            {
                var eventToUpdate = await _db.Events.FindAsync(_event.EventId);
                if (eventToUpdate != null)
                {
                    eventToUpdate.EventName = _event.EventName;
                    eventToUpdate.Description = _event.Description;
                    eventToUpdate.StartDate = _event.StartDate;
                    eventToUpdate.EndDate = _event.EndDate;
                    eventToUpdate.EventStartingPrice = _event.EventStartingPrice;
                    eventToUpdate.EventEndingPrice = _event.EventEndingPrice;
                    eventToUpdate.IsFree = _event.IsFree;
                    eventToUpdate.IsOffline = _event.IsOffline;
                    eventToUpdate.Location = _event.Location;
                    eventToUpdate.IsPublished = _event.IsPublished;
                    eventToUpdate.IsActive = _event.IsActive;
                    eventToUpdate.CategoryId = _event.CategoryId;
                    eventToUpdate.OrganisationId = _event.OrganisationId;
                    eventToUpdate.AcceptedBy = _event.AcceptedBy;
                    eventToUpdate.CreatedBy = _event.CreatedBy;
                    eventToUpdate.UpdatedBy = _event.UpdatedBy;
                    eventToUpdate.UpdatedOn = _event.UpdatedOn;
                    await _db.SaveChangesAsync();
                    await _db.Entry(eventToUpdate).ReloadAsync();
                    return (eventToUpdate, "Event Updated Successfully");
                }
                else
                {
                    return (new Event(), "Event Not Found");
                }
            }
            catch (Exception ex)
            {
                return (new Event(), "Event Not Updated");
            }
        }

        public async Task<Event> UpdateEventRegistrationStatus(Guid eventId, byte registrationStatusId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                var _event = await _db.Events.FindAsync(eventId);
                if (_event != null)
                {
                    _event.RegistrationStatusId = registrationStatusId;
                    //_event.UpdatedBy = updatedBy;
                    //_event.updatedAt = updatedAt;
                    await _db.SaveChangesAsync();
                    await _db.Entry(_event).ReloadAsync();
                    return _event;
                }
                else
                {
                    return new Event();
                }
            }
            catch (Exception ex)
            {
                return new Event();
            }
        }

        public async Task<Event> UpdateIsCancelledEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                var _event = await _db.Events.FindAsync(eventId);
                if (_event != null)
                {
                    _event.IsCancelled = !_event.IsCancelled;
                    //_event.UpdatedBy = updatedBy;
                    //_event.updatedAt = updatedAt;
                    await _db.SaveChangesAsync();
                    await _db.Entry(_event).ReloadAsync();
                    return _event;
                }
                else
                {
                    return new Event();
                }
            }
            catch (Exception ex)
            {
                return new Event();
            }
        }

        public async Task<Event> UpdateIsPublishedEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                var _event = await _db.Events.FindAsync(eventId);
                if (_event != null)
                {
                    _event.IsPublished = !_event.IsPublished;
                    //_event.UpdatedBy = updatedBy;
                    //_event.updatedAt = updatedAt;
                    await _db.SaveChangesAsync();
                    await _db.Entry(_event).ReloadAsync();
                    return _event;
                }
                else
                {
                    return new Event();
                }
            }
            catch (Exception ex)
            {
                return new Event();
            }
        }

        public async Task<Event> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                var _event = await _db.Events.FindAsync(eventId);
                if (_event != null)
                {
                    //_event.RejectedBy = rejectedBy;
                    //_event.UpdatedBy = updatedBy;
                    //_event.updatedAt = updatedAt;
                    await _db.SaveChangesAsync();
                    await _db.Entry(_event).ReloadAsync();
                    return _event;
                }
                else
                {
                    return new Event();
                }
            }
            catch (Exception ex)
            {
                return new Event();
            }
        }
    }
}
