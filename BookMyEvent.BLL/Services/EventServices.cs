using AutoMapper;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookMyEvent.BLL.Services
{
    internal class EventServices : IEventServices
    {
        private readonly IEventRepository eventRepository;
        private readonly IOrganiserFormServices organiserFormServices;
        private readonly IEventImageRepository eventImageRepository;
        private Mapper _mapper;
        public EventServices(IEventRepository service,IOrganiserFormServices _organiserFormServices,IEventImageRepository _eventImageRepository)
        {
            eventRepository = service;
            organiserFormServices = _organiserFormServices;
            eventImageRepository = _eventImageRepository;
            _mapper = Automapper.InitializeAutomapper();

        }

        public async Task<(BLEvent _NewEvent,string message)> Add((BLEvent EventDetails, List<BLRegistrationFormFields> RegistrationFormFields,BLForm EventForm,List<BLEventImages> EventImages) NewEvent)
        {
            try
            {
                if(NewEvent.EventDetails != null)
                {

                    (Guid FormId, string Message) AddForms = await organiserFormServices.AddForm(NewEvent.EventForm, NewEvent.RegistrationFormFields);
                    if(AddForms.FormId==Guid.Empty) { return (null,AddForms.Message); }
                    NewEvent.EventDetails.FormId = AddForms.FormId;
                    Event NewEventDL = _mapper.Map<Event>(NewEvent.EventDetails);
                    
                    Event EventDL=await eventRepository.AddEvent(NewEventDL);
                    if (EventDL == null)
                    {
                        return (null, "Failed to Add New Event");
                    }
                    BLEvent bLEvent=_mapper.Map<BLEvent>(EventDL);




                    List<EventImage> imagesDL = new List<EventImage>();
                    foreach(var image in NewEvent.EventImages)
                    {
                        EventImage imageDL = _mapper.Map<EventImage>(image);
                        imageDL.EventId = bLEvent.EventId;
                        imagesDL.Add(imageDL);
                    }
                    bool isImagesAdded= await eventImageRepository.AddMultipleImages(imagesDL);
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
                return (null,"Failed to New Event");
            }
        }
        public async Task<BLEvent> GetEventById(Guid EventId)
        {
            try
            {
                Event Event = await eventRepository.GetEventById(EventId);
                if (Event == null) { return null; }
                BLEvent EventBL= _mapper.Map<BLEvent>(Event);
            
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
                Event Event = _mapper.Map<Event>(_event);
                return (_mapper.Map<BLEvent>(await eventRepository.UpdateEvent(Event)), "Event Updated");
            }
            catch (Exception ex)
            {
                return (new BLEvent(), "Error in the Event Services ");
            }
        }
        public async Task<(bool, string Message)> DeleteEvent(Guid id)
        {
            try
            {
                return (await eventRepository.DeleteEvent(id));
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
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByCategoryId(byte categoryId)
        {
            try
            {
                List<BLEvent> AllEvents = _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByCategoryId(categoryId));
              
                return AllEvents;
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
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByStartDate(DateTime date)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByStartDate(date));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByEndDate(DateTime date)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByEndDate(date));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByStartDateAndEndDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByStartDateAndEndDate(startDate, endDate));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingMoreThanPrice(decimal startingPrice)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingMoreThanPrice(startingPrice));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingLessThanPrice(decimal endingPrice)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingLessThanPrice(endingPrice));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingPriceRange(decimal startingPrice, decimal endingPrice)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingPriceRange(startingPrice, endingPrice));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingName(string name)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingName(name));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByMode(bool isOffline)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByMode(isOffline));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedIsFreeEvents(bool isFree)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedIsFreeEvents(isFree));
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLEvent>> GetAllActiveEventsByIsPublished(bool isPublished)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActiveEventsByIsPublished(isPublished));
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
                return _mapper.Map<BLEvent>(await eventRepository.UpdateEventRegistrationStatus(eventId, registrationStatusId, updatedBy, updatedAt));
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
                return _mapper.Map<BLEvent>(await eventRepository.UpdateIsCancelledEvent(eventId, updatedBy, updatedAt));
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
                return _mapper.Map<BLEvent>(await eventRepository.UpdateIsPublishedEvent(eventId, updatedBy, updatedAt));
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
        public async Task<BLEvent> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateRejectedBy(eventId, rejectedBy, updatedBy, updatedAt));
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

        public async Task<List<BLEvent>> GetFilteredEvents(DateTime startDate, DateTime endDate, decimal startPrice, decimal endPrice, string location, bool isFree, List<int> categoryIds, int pageNumber, int pageSize)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetFilteredEvents(startDate, endDate, startPrice, endPrice, location, isFree, categoryIds, pageNumber,pageSize));
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to fetch filtered events.", ex);
            }
        }
    }
}
