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

        public async Task<(bool,string)> Delete(Guid EventId)
        {
            try
            {
                return await eventRepository.DeleteEvent(EventId);
            }
            catch
            {
                return (false, "Issue in the BLL");
            }
        }

        public async Task<List<BLEvent>> GetAllActivePublishedEvents()
        {
            try
            {
                return _mapper.Map<List<BLEvent>>(await eventRepository.GetAllActivePublishedEvents());
            }
            catch
            {
                return null;
            }
        }

        public async Task<BLEvent> Update(BLEvent bLEvent)
        {
            try
            {
                return _mapper.Map<BLEvent>(await eventRepository.UpdateEvent(_mapper.Map<Event>(bLEvent)));
            }
            catch
            {
                return null;
            }
        }
    }
}
