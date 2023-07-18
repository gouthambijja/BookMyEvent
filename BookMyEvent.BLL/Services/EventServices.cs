using AutoMapper;
using BookMyEvent.BLL.Utilities;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookMyEvent.BLL.Services
{
    public class EventServices : IEventServices
    {
        private readonly IEventRepository eventRepository;
        private readonly IOrganiserFormServices organiserFormServices;
        private readonly IEventImageRepository eventImageRepository;
        private readonly Mapper _mapper;
        public EventServices(IEventRepository service, IOrganiserFormServices _organiserFormServices, IEventImageRepository _eventImageRepository)
        {
            eventRepository = service;
            organiserFormServices = _organiserFormServices;
            eventImageRepository = _eventImageRepository;
            _mapper = Automapper.InitializeAutomapper();
        }

        public async Task<(BLEvent _NewEvent, string message)> Add(BLEvent EventDetails, List<BLEventImages> EventImages)
        {
            try
            {
                if (EventDetails != null)
                {
                    Event EventDL = await eventRepository.AddEvent(_mapper.Map<Event>(EventDetails));
                    if (EventDL == null)
                    {
                        return (null, "Failed to Add New Event");
                    }
                    BLEvent bLEvent = _mapper.Map<BLEvent>(EventDL);
                    List<EventImage> imagesDL = new List<EventImage>();
                    foreach (var image in EventImages)
                    {
                        EventImage imageDL = _mapper.Map<EventImage>(image);
                        imageDL.EventId = bLEvent.EventId;
                        imageDL.ImgId = Guid.NewGuid();
                        imagesDL.Add(imageDL);
                    }
                    bool isImagesAdded = await eventImageRepository.AddMultipleImages(imagesDL);
                    if (!isImagesAdded)
                    {
                        return (bLEvent, "Failed to Add Event Images");
                    }
                    return (bLEvent, "Added New Event Successfully");

                }
                else
                {
                    return (null, "Failed to New Event");

                }
            }
            catch
            {
                return (null, "Failed to New Event");
            }
        }
        public async Task<BLEvent> GetEventById(Guid EventId)
        {
            try
            {
                Event Event = await eventRepository.GetEventById(EventId);
                if (Event == null) { return null; }
                BLEvent EventBL = _mapper.Map<BLEvent>(Event);
                return EventBL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<(BLEvent, string Message)> UpdateEvent(BLEvent _event)
        {
            try
            {
                // Event event = _mapper.Map<Event>(_event);
                //Event updatedEvent= await eventRepository.UpdateEvent(event);
                // BLEvent eventBL=_mapper.Map<BLEvent>(updatedEvent);
                // return (eventBL, "Event Updated");
                Event eventDL = _mapper.Map<Event>(_event);
                var result = await eventRepository.UpdateEvent(eventDL);
                if (result.Item1 != null)
                {
                    BLEvent eventBL = _mapper.Map<BLEvent>(result.Item1);
                    return (eventBL, "Event Updated");
                }
                else
                {
                    return (null, result.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (new BLEvent(), "Error in the Event Services ");
            }
        }
        public async Task<(bool, string Message)> DeleteEvent(Guid id)
        {
            try
            {
                return await eventRepository.DeleteEvent(id);
            }
            catch (Exception ex)
            {
                return (false, "error in the event services");
            }
        }

        public async Task<List<BLEvent>> GetAllActivePublishedEvents(int pageNumber, int pageSize)
        {
            try
            {
                List<Event> events = await eventRepository.GetAllActivePublishedEvents(pageNumber, pageSize);
                List<BLEvent> mappedEvents = _mapper.Map<List<BLEvent>>(events);
                return mappedEvents;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByOrgId(Guid orgId)
        {
            try
            {
                List<BLEvent> AllEvents = _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByOrgId(orgId));

                return AllEvents;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByLocation(string location)
        {
            try
            {
                List<BLEvent> AllEvents = _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByLocation(location));

                return AllEvents;
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLEvent> UpdateEventRegistrationStatus(Guid eventId, byte registrationStatusId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                Event eventDL = await eventRepository.UpdateEventRegistrationStatus(eventId, registrationStatusId, updatedBy, updatedAt);
                if (eventDL != null)
                {
                    BLEvent eventBL = _mapper.Map<BLEvent>(eventDL);
                    return eventBL;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLEvent> UpdateIsCancelledEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                Event eventDL = await eventRepository.UpdateIsCancelledEvent(eventId, updatedBy, updatedAt);
                if (eventDL != null)
                {
                    BLEvent bLEvent = _mapper.Map<BLEvent>(eventDL);
                    return bLEvent;
                }
                return null;

            }
            catch
            {
                return null;
            }
        }
        public async Task<BLEvent> UpdateIsPublishedEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                Event eventDL = await eventRepository.UpdateIsPublishedEvent(eventId, updatedBy, updatedAt);
                if (eventDL != null)
                {
                    BLEvent bLEvent = _mapper.Map<BLEvent>(eventDL);
                    return bLEvent;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLEvent> UpdateAcceptedBy(Guid eventId, Guid acceptBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateAcceptedBy(eventId, acceptBy, updatedBy, updatedAt));
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLEvent> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt, string reason)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateRejectedBy(eventId, rejectedBy, updatedBy, updatedAt, reason));
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BLEvent>> GetAllCreatedEventsByOrganisation(Guid orgId)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllCreatedEventsByOrganisation(orgId));

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BLEvent>> GetAllCreatedEventsByOrganiser(Guid organiserId)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllCreatedEventsByOrganiser(organiserId));

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BLEvent>> GetFilteredEvents(DateTime? startDate, DateTime? endDate, decimal? startPrice, decimal? endPrice, string? location, string? name, bool? isFree, List<int>? categoryIds, int pageNumber, int? pageSize)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetFilteredEvents(startDate, endDate, startPrice, endPrice, location, name, isFree, categoryIds, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch filtered events.", ex);
            }
        }

        public async Task<(bool isActiveUpdated, string message)> SoftDelete(Guid eventId, Guid updatedBy, DateTime updatedOn)
        {
            try
            {
                return await eventRepository.UpdateIsActive(eventId, updatedBy, updatedOn);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete event.", ex);
            }
        }

        public async Task<List<BLEvent>> GetAllPastEventsByOrganisationId(Guid organisationId, int pageNumber, int pageSize)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetPastEventsByOrganisation(organisationId, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch past events.", ex);
            }
        }

        public async Task<List<BLEvent>> GetAllPastEventsByOrganiserId(Guid organisationId, int pageNumber, int pageSize)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetPastEventsByOrganiser(organisationId, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch past events.", ex);
            }
        }
        public async Task<(int NoOfOrganiserPastEvents, int NoOfOrganisationPastEvents)> GetNoOfPastEvents(Guid organiserId, Guid organisationId)
        {
            try
            {
                int organiserPastEvents = await eventRepository.GetOrganiserTotalNoOfPastEvents(organiserId);
                int organisationPastEvents = await eventRepository.GetOrganisationTotalNoOfPastEvents(organisationId);
                return (organiserPastEvents, organisationPastEvents);
            }
            catch
            {
                return (0, 0);
            }
        }
        public async Task<List<BLEvent>> GetOrganiserRequestedEvents(Guid organiserId)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetOrganiserRequestedEvents(organiserId));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch organiser requested events.", ex);
            }
        }

        public async Task<List<BLEvent>> GetOrganisationRequestedEvents(Guid organisationId)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetOrganisationRequestedEvents(organisationId));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch organisation requested events.", ex);
            }
        }

        public async Task<int> GetNoOfOrganisationRequestedEvents(Guid organisationId)
        {
            try
            {
                return await eventRepository.GetNoOfOrganisationRequestedEvents(organisationId);
            }
            catch { return 0; }
        }
        public async Task<List<BLEventImages>> GetEventImages(Guid eventId)
        {
            try
            {
                List<EventImage> eventImagesDL = await eventImageRepository.GetImagesByEventId(eventId);
                return _mapper.Map<List<BLEventImages>>(eventImagesDL);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateEventAvailableSeats(Guid eventId, int availableSeats)
        {
            try
            {
                var isSuccess = await eventRepository.UpdateEventAvailableSeats(eventId, availableSeats);
                if (isSuccess)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
