using AutoFixture;
using AutoFixture.Kernel;
using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.MockData
{
    public class EventsMockData
    {

        public static Event GetDLEvent(Guid eventId)
        {
            Event eventDL = new Event()
            {
                EventId = eventId,
                EventName = "EtchSpace",
                EventEndingPrice = 200,
                EventStartingPrice = 200,
                CategoryId = 1,
                Capacity = 200,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Description = "ujbuwdc wndciwdc iwdnci",

            };
            return eventDL;

        }
        public static BLEvent GetBLEvent(Guid eventId)
        {
            BLEvent eventDL = new BLEvent()
            {
                EventId = eventId,
                EventName = "EtchSpace",
                EventEndingPrice = 200,
                EventStartingPrice = 200,
                CategoryId = 1,
                Capacity = 200,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Description = "ujbuwdc wndciwdc iwdnci",
            };
            return eventDL;
        }
        public static List<Event> GetListOfDLEvents()
        {
           
            List<Event> eventDLList = new List<Event>()
            {
                 new Event
                 {
                    EventId = Guid.NewGuid(),
                    EventName = "EtchSpace",
                    EventEndingPrice = 200,
                    EventStartingPrice = 200,
                    CategoryId = 1,
                    Capacity = 200,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Description = "ujbuwdc wndciwdc iwdnci"
                 },
                 new Event
                 {
                    EventId = Guid.NewGuid(),
                    EventName = "EtchSpace",
                    EventEndingPrice = 200,
                    EventStartingPrice = 200,
                    CategoryId = 1,
                    Capacity = 200,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Description = "ujbuwdc wndciwdc iwdnci"
                 },
                 new Event
                 {
                    EventId=Guid.NewGuid(),
                    EventName = "eferSpace",
                    EventEndingPrice = 200,
                    EventStartingPrice = 200,
                    CategoryId = 3,
                    Capacity = 200,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Description = "ujbuwdc wndciwdc iwdnci"
                 }
            };
            return eventDLList;
        }
    }
}

