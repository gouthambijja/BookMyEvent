using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Event>> GetAllActivePublishedEvents(int pageNumber, int pageSize)
        {
            try
            {
                int skipCount = (pageNumber - 1) * pageSize;

                var events = _db.Events
                    .Where(x => x.IsActive == true && x.IsPublished && x.RegistrationStatusId != 3)
                    .Skip(skipCount)
                    .Take(pageSize)
                    .ToList();

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
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.CategoryId == categoryId && x.RegistrationStatusId != 3).ToList();
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
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.EndDate <= date && x.RegistrationStatusId != 3).ToList();
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
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.RegistrationStatusId != 3).ToList();
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
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.Location == location && x.RegistrationStatusId != 3).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        //public async Task<List<Event>> GetAllActivePublishedEventsByMode(bool isOffline)
        //{
        //    try
        //    {
        //        var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.IsOffline == isOffline && x.RegistrationStatusId != 3).ToList();
        //        return events;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<Event>();
        //    }
        //}

        public async Task<List<Event>> GetAllActivePublishedEventsByStartDate(DateTime date)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.StartDate >= date && x.RegistrationStatusId != 3).ToList();
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
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.StartDate >= startDate && x.EndDate <= endDate && x.RegistrationStatusId != 3).ToList();
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
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.OrganisationId == orgId && x.RegistrationStatusId != 3).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingLessThanPrice(decimal endingPrice)
        {

            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.EventEndingPrice <= endingPrice && x.RegistrationStatusId != 3).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }

        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingMoreThanPrice(decimal startingPrice)
        {

            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.EventStartingPrice >= startingPrice && x.RegistrationStatusId != 3).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }

        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingName(string name)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.EventName.Contains(name) && x.RegistrationStatusId != 3).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllActivePublishedEventsHavingPriceRange(decimal startingPrice, decimal endingPrice)
        {
            try
            {
                var events = _db.Events.Where(x => x.IsActive == true && x.IsPublished && x.EventStartingPrice >= startingPrice && x.EventEndingPrice <= endingPrice && x.RegistrationStatusId != 3).ToList();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
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
                    eventToUpdate.Country = _event.Country;
                    eventToUpdate.State = _event.State;
                    eventToUpdate.City = _event.City;
                    eventToUpdate.EventStartingPrice = _event.EventStartingPrice;
                    eventToUpdate.EventEndingPrice = _event.EventEndingPrice;
                    eventToUpdate.IsFree = _event.IsFree;
                    //eventToUpdate.IsOffline = _event.IsOffline;
                    eventToUpdate.Location = _event.Location;
                    eventToUpdate.IsPublished = _event.IsPublished;
                    eventToUpdate.IsActive = _event.IsActive;
                    eventToUpdate.CategoryId = _event.CategoryId;
                    eventToUpdate.OrganisationId = _event.OrganisationId;
                    eventToUpdate.AcceptedBy = _event.AcceptedBy;
                    eventToUpdate.CreatedBy = _event.CreatedBy;
                    eventToUpdate.UpdatedBy = _event.UpdatedBy;
                    eventToUpdate.UpdatedOn = DateTime.Now;
                    eventToUpdate.RegistrationStatusId = _event.RegistrationStatusId;
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
                    _event.RejectedBy = rejectedBy;
                    _event.UpdatedBy = updatedBy;
                    _event.UpdatedOn = updatedAt;
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

        public async Task<List<Event>> GetAllCreatedEventsByOrganisation(Guid orgId)
        {
            try
            {
                var events = await _db.Events.Where(x => x.IsActive == true && x.EndDate >= DateTime.Now && x.OrganisationId == orgId && x.AcceptedBy != null).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetAllCreatedEventsByOrganiser(Guid organiserId)
        {
            try
            {
                var events = await _db.Events.Where(x => x.IsActive == true && x.EndDate >= DateTime.Now && x.CreatedBy == organiserId && x.AcceptedBy != null).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();
            }
        }

        public async Task<List<Event>> GetFilteredEvents(DateTime? startDate, DateTime? endDate, decimal? startPrice, decimal? endPrice, string? location, bool? isFree, List<int>? categoryIds, int pageNumber, int? pageSize)
        {
            try
            {
                // Perform the filtering based on the provided criteria
                var filteredEvents = await _db.Events
                    .Where(e => e.StartDate >= startDate &&
                    (e.EndDate <= endDate && e.EndDate >= DateTime.Now) &&
                    e.EventStartingPrice >= startPrice &&
                    e.EventEndingPrice <= endPrice &&
                    ((location != "" && location != null) ? (e.Location.Contains(location) || e.State.Contains(location) || e.Country.Contains(location) || e.City.Contains(location)) : true) &&
                    (isFree == true ? e.IsFree : true) &&
                    (categoryIds == null || categoryIds.Count() == 0 || categoryIds.Contains(e.CategoryId)) &&
                    e.IsActive == true &&
                    e.IsPublished == true &&
                    e.RegistrationStatusId != 3)
                    .Skip((pageNumber - 1) * (int)pageSize)
                    .Take((int)pageSize)
                    .ToListAsync();
                return filteredEvents;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine(ex);
                throw new Exception("Failed to fetch filtered events from the database.", ex);
            }
        }

        public async Task<(bool isActiveUpdated, string message)> UpdateIsActive(Guid eventId, Guid updatedBy, DateTime updatedOn)
        {
            try
            {
                var eventToUpdate = await _db.Events.FindAsync(eventId);
                if (eventToUpdate != null)
                {
                    eventToUpdate.IsActive = false;
                    eventToUpdate.UpdatedBy = updatedBy;
                    eventToUpdate.UpdatedOn = updatedOn;
                    await _db.SaveChangesAsync();
                    await _db.Entry(eventToUpdate).ReloadAsync();
                    return (true, "Event Deleted Successfully");
                }
                else
                {
                    return (false, "Event Not Found");
                }
            }
            catch (Exception ex)
            {
                return (false, "Event Not Updated");
            }
        }

        public async Task<List<Event>> GetPastEventsByOrganiser(Guid organiserId, int pageNumber, int pageSize)
        {
            try
            {
                var events = await _db.Events
                    .Where(x => x.CreatedBy == organiserId &&
                    (x.EndDate < DateTime.Now || x.IsCancelled) &&
                    (x.AcceptedBy != null || x.RejectedBy != null))
                    .OrderByDescending(x => x.EndDate) // Sort by latest end date
                    .Skip((pageNumber - 1) * pageSize) // Skip events on previous pages
                    .Take(pageSize) // Take events for the current page
                    .ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch past events from the database.", ex);
            }
        }

        public async Task<List<Event>> GetPastEventsByOrganisation(Guid organisationId, int pageNumber, int pageSize)
        {
            try
            {
                var events = await _db.Events
                    .Where(x => x.OrganisationId == organisationId &&
                                       (x.EndDate < DateTime.Now || x.IsCancelled) &&
                                                          (x.AcceptedBy != null || x.RejectedBy != null))
                    .OrderByDescending(x => x.EndDate) // Sort by latest end date
                    .Skip((pageNumber - 1) * pageSize) // Skip events on previous pages
                    .Take(pageSize) // Take events for the current page
                    .ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch past events from the database.", ex);
            }
        }
        public async Task<int> GetOrganiserTotalNoOfPastEvents(Guid organiserId)
        {
            try
            {
                var events = await _db.Events
                   .CountAsync(x => x.CreatedBy == organiserId &&
                   (x.EndDate < DateTime.Now || x.IsCancelled) &&
                   (x.AcceptedBy != null || x.RejectedBy != null));
                return events;

            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> GetOrganisationTotalNoOfPastEvents(Guid organisationId)
        {
            try
            {
                var events = await _db.Events
                   .CountAsync(x => x.OrganisationId == organisationId &&
                                       (x.EndDate < DateTime.Now || x.IsCancelled) &&
                                                          (x.AcceptedBy != null || x.RejectedBy != null));
                return events;

            }
            catch
            {
                return 0;
            }
        }
        public async Task<List<Event>> GetOrganiserRequestedEvents(Guid organiserId)
        {
            try
            {
                var events = await _db.Events.Where(x => x.CreatedBy == organiserId && x.AcceptedBy == null && x.RejectedBy == null && x.IsActive == true).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch requested events from the database.", ex);
            }
        }


        public Task<List<Event>> GetOrganisationRequestedEvents(Guid organisationId)
        {
            try
            {
                var events = _db.Events.Where(x => x.OrganisationId == organisationId && x.AcceptedBy == null && x.RejectedBy == null && x.IsActive == true).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch requested events from the database.", ex);
            }
        }

        public async Task<List<Event>> GetEventsByEventIds(List<Guid> eventIds)
        {
            try
            {
                var Events = await _db.Events.Where(e => eventIds.Contains(e.EventId)).ToListAsync();
                return Events;
            }
            catch
            {
                return null;
            }
        }
    }
}
