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
        private Mapper _mapper;
        public EventServices(IEventRepository service)
        {
            eventRepository = service;
            _mapper = Automapper.InitializeAutomapper();

        }

        public Task<BLEvent> Add((BLEvent EventDetails, List<(BLUserInputForm, List<BLUserInputFormField>)> RegistrationFormDetails) Event)
        {
            try
            {
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLEvent> GetEventById(Guid EventId)
        {
            try
            {
                Event Event = await eventRepository.GetEventById(EventId);
                return _mapper.Map<BLEvent>(Event);
            }
            catch (Exception ex)
            {
                return new BLEvent();
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
        public async Task<List<BLEvent>> GetAllActivePublishedEvents()
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEvents());
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByCategoryId(byte categoryId)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByCategoryId(categoryId));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByOrgId(Guid orgId)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByOrgId(orgId));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByLocation(string location)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByLocation(location));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByStartDate(DateTime date)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByStartDate(date));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByEndDate(DateTime date)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByEndDate(date));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByStartDateAndEndDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByStartDateAndEndDate(startDate, endDate));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingMoreThanPrice(decimal startingPrice)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingMoreThanPrice(startingPrice));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingLessThanPrice(decimal endingPrice)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingLessThanPrice(endingPrice));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingPriceRange(decimal startingPrice, decimal endingPrice)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingPriceRange(startingPrice, endingPrice));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsHavingName(string name)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsHavingName(name));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedEventsByMode(bool isOffline)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEventsByMode(isOffline));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActivePublishedIsFreeEvents(bool isFree)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedIsFreeEvents(isFree));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<List<BLEvent>> GetAllActiveEventsByIsPublished(bool isPublished)
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActiveEventsByIsPublished(isPublished));
            }
            catch (Exception ex)
            {
                return new List<BLEvent>();
            }
        }
        public async Task<BLEvent> UpdateEventRegistrationStatus(Guid eventId, byte registrationStatusId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateEventRegistrationStatus(eventId, registrationStatusId, updatedBy, updatedAt));
            }
            catch (Exception ex)
            {
                return new BLEvent();
            }
        }
        public async Task<BLEvent> UpdateIsCancelledEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateIsCancelledEvent(eventId, updatedBy, updatedAt));
            }
            catch (Exception ex)
            {
                return new BLEvent();
            }
        }
        public async Task<BLEvent> UpdateIsPublishedEvent(Guid eventId, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateIsPublishedEvent(eventId, updatedBy, updatedAt));
            }
            catch (Exception ex)
            {
                return new BLEvent();
            }
        }
        public async Task<BLEvent> UpdateAcceptedBy(Guid eventId, Guid acceptBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateAcceptedBy(eventId, acceptBy, updatedBy, updatedAt));
            }
            catch (Exception ex)
            {
                return new BLEvent();
            }
        }
        public async Task<BLEvent> UpdateRejectedBy(Guid eventId, Guid rejectedBy, Guid updatedBy, DateTime updatedAt)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateRejectedBy(eventId, rejectedBy, updatedBy, updatedAt));
            }
            catch (Exception ex)
            {
                return new BLEvent();
            }
        }

    }
}
