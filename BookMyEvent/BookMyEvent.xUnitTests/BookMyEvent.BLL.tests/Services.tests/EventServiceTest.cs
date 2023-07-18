using BookMyEvent.BLL.Contracts;
using BookMyEvent.DLL.Contracts;
using BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.MockData;
using Moq;
using BookMyEvent.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.WebApi.Controllers;
using db.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using AutoMapper;
using BookMyEvent.BLL.Utilities;


namespace BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.Services.tests
{
    public class EventServiceTest
    {
        private readonly Mock<IEventRepository> _eventRepoMock;

        private readonly Mapper _mapper;

        public EventServiceTest()
        {
            _eventRepoMock = new Mock<IEventRepository>();
            _mapper = Automapper.InitializeAutomapper();
        }
        [Fact]
        public async void GetEventById_SuccessTest()
        {
            //Arrange
            var id = Guid.NewGuid();
            var eventDL = EventsMockData.GetDLEvent(id);
            _eventRepoMock.Setup(x => x.GetEventById(id)).ReturnsAsync(eventDL);
            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetEventById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(eventDL.EventName, result.EventName);
        }

        [Fact]
        public async void GetEventById_FailureTest()
        {
            //Arrange
            var id = Guid.NewGuid();
            var eventDL = EventsMockData.GetDLEvent(id);

            _eventRepoMock.Setup(x => x.GetEventById(id)).ReturnsAsync((Event)null);
            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetEventById(id);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void GetEventById_ExceptionTest()
        {
            //Arrange
            var id = Guid.NewGuid();
            var eventDL = EventsMockData.GetDLEvent(id);

            _eventRepoMock.Setup(x => x.GetEventById(id)).ReturnsAsync((Event)null);
            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetEventById(id);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void UpdateEvent_SuccessTest()
        {
            //Arrange
            var id = Guid.NewGuid();
            var eventDL = EventsMockData.GetDLEvent(id);
            var eventBL = EventsMockData.GetBLEvent(id);

            _eventRepoMock.Setup(x => x.UpdateEvent(It.IsAny<Event>())).ReturnsAsync((eventDL, "Updated"));

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);
            //Act
            var result = await eventServices.UpdateEvent(eventBL);
            //Assert
            Assert.NotNull(result.Item1);
            Assert.Equal("Event Updated", result.Message);
            Assert.Equal(eventBL.EventName, result.Item1.EventName);
            //Assert.Equal(eventBL, result.Item1);
        }

        [Fact]
        public async void UpdateEvent_FailureTest()
        {
            //Arrange
            var id = Guid.NewGuid();
            var eventDL = EventsMockData.GetDLEvent(id);
            var eventBL = EventsMockData.GetBLEvent(id);

            _eventRepoMock.Setup(x => x.UpdateEvent(It.IsAny<Event>())).ReturnsAsync((null, "Event Not Found"));

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateEvent(eventBL);

            //Assert
            Assert.Null(result.Item1);
            Assert.Equal("Event Not Found", result.Message);

        }
        [Fact]
        public async void DeleteEvent_SuccessTest()
        {
            //Arrange
            var id = Guid.NewGuid();
            _eventRepoMock.Setup(x => x.DeleteEvent(It.IsAny<Guid>())).ReturnsAsync((true, "Event Deleted Successfully"));

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.DeleteEvent(id);

            //Assert
            Assert.True(result.Item1);
            Assert.Equal("Event Deleted Successfully", result.Message);

        }

        [Fact]
        public async void DeleteEvent_FailureTest()
        {
            //Arrange
            var id = Guid.NewGuid();

            _eventRepoMock.Setup(x => x.DeleteEvent(It.IsAny<Guid>())).ReturnsAsync((false, "Event Not Found"));

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.DeleteEvent(id);

            //Assert
            Assert.False(result.Item1);
            Assert.Equal("Event Not Found", result.Message);

        }

        [Fact]
        public async void GetAllActivePublishedEVents_SuccessTest()
        {
            //Arrange
            int pageNumber = 1;
            int pageSize = 10;
            var DLEventsList = EventsMockData.GetListOfDLEvents();
            _eventRepoMock.Setup(x => x.GetAllActivePublishedEvents(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(DLEventsList);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetAllActivePublishedEvents(pageNumber, pageSize);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(DLEventsList.Count, result.Count);

        }


        //[Fact]
        //public async void GetAllActivePublishedEVents_FailureTest()
        //{
        //    //Arrange
        //    int pageNumber = 1;
        //    int pageSize = 10;
        //    var DLEventsList = EventsMockData.GetListOfDLEvents();
        //    _eventRepoMock.Setup(x => x.GetAllActivePublishedEvents(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(DLEventsList);

        //    var eventServices = new EventServices(_eventRepoMock.Object, null, null);

        //    //Act
        //    var result = await eventServices.GetAllActivePublishedEvents(pageNumber, pageSize);

        //    //Assert
        //    Assert.NotEmpty(result);
        //    Assert.Equal(DLEventsList.Count, result.Count);

        //}

        [Fact]
        public async void GetAllActivePublishedEventsByOrgId_SuccessTest()
        {
            //Arrange
            var orgId = Guid.NewGuid();
            var DLEventsList = EventsMockData.GetListOfDLEvents();
            _eventRepoMock.Setup(x => x.GetAllActivePublishedEventsByOrgId(It.IsAny<Guid>())).ReturnsAsync(DLEventsList);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetAllActivePublishedEventsByOrgId(orgId);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(DLEventsList.Count, result.Count);

        }

        [Fact]
        public async void GetAllActivePublishedEventsByLocation_SuccessTest()
        {
            //Arrange
            string location = "Hyderabad";
            var DLEventsList = EventsMockData.GetListOfDLEvents();
            _eventRepoMock.Setup(x => x.GetAllActivePublishedEventsByLocation(It.IsAny<string>())).ReturnsAsync(DLEventsList);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetAllActivePublishedEventsByLocation(location);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(DLEventsList.Count, result.Count);

        }

        [Fact]
        public async void UpdateEventRegistrationStatus_SuccessTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            byte registrationstatusId = 1;
            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateEventRegistrationStatus(It.Is<Guid>(x => x == eventId), It.Is<byte>(x => x == registrationstatusId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync(EventDL);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateEventRegistrationStatus(eventId, registrationstatusId, updatedBy, updatedAt);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(eventId, result.EventId);

        }

        [Fact]
        public async void UpdateEventRegistrationStatus_FailureTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            byte registrationstatusId = 1;
            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateEventRegistrationStatus(It.Is<Guid>(x => x == eventId), It.Is<byte>(x => x == registrationstatusId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync((Event)null);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateEventRegistrationStatus(eventId, registrationstatusId, updatedBy, updatedAt);

            //Assert
            Assert.Null(result);


        }

        [Fact]
        public async void UpdateIsCancelledEvent_SuccessTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();

            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateIsCancelledEvent(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync(EventDL);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateIsCancelledEvent(eventId, updatedBy, updatedAt);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(eventId, result.EventId);

        }

        [Fact]
        public async void UpdateIsCancelledEvent_FailureTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();

            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateIsCancelledEvent(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync((Event)null);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateIsCancelledEvent(eventId, updatedBy, updatedAt);

            //Assert
            Assert.Null(result);


        }
        [Fact]
        public async void UpdateIsPublishedEvent_SuccessTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();

            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateIsPublishedEvent(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync(EventDL);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateIsPublishedEvent(eventId, updatedBy, updatedAt);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(eventId, result.EventId);

        }

        [Fact]
        public async void UpdateIsPublishedEvent_FailureTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();

            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateIsPublishedEvent(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync((Event)null);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateIsPublishedEvent(eventId, updatedBy, updatedAt);

            //Assert
            Assert.Null(result);


        }
        [Fact]
        public async void UpdateAcceptedBy_SuccessTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            Guid acceptedBy = Guid.NewGuid();
            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateAcceptedBy(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == acceptedBy), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync(EventDL);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateAcceptedBy(eventId, acceptedBy, updatedBy, updatedAt);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(eventId, result.EventId);

        }

        [Fact]
        public async void UpdateAcceptedBy_FailureTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            Guid acceptedBy = Guid.NewGuid();
            Guid updatedBy = Guid.NewGuid();
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateAcceptedBy(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == acceptedBy), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt))).ReturnsAsync((Event)null);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateAcceptedBy(eventId, acceptedBy, updatedBy, updatedAt);

            //Assert
            Assert.Null(result);


        }

        [Fact]
        public async void UpdateRejectedBy_SuccessTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            Guid rejectedBy = Guid.NewGuid();
            Guid updatedBy = Guid.NewGuid();
            string reason = "string string";
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateRejectedBy(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == rejectedBy), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt), It.Is<string>(x => x == reason))).ReturnsAsync(EventDL);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateRejectedBy(eventId, rejectedBy, updatedBy, updatedAt, reason);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(eventId, result.EventId);

        }

        [Fact]
        public async void UpdateRejectedBy_FailureTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            Guid rejectedBy = Guid.NewGuid();
            Guid updatedBy = Guid.NewGuid();
            string reason = "string string";
            DateTime updatedAt = DateTime.Now;
            var EventDL = EventsMockData.GetDLEvent(eventId);
            _eventRepoMock.Setup(x => x.UpdateRejectedBy(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == rejectedBy), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updatedAt), It.Is<string>(x => x == reason))).ReturnsAsync((Event)null);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.UpdateRejectedBy(eventId, rejectedBy, updatedBy, updatedAt, reason);

            //Assert
            Assert.Null(result);


        }

        [Fact]
        public async void GetAllCreatedEventsByOrganisation_SuccessTest()
        {
            //Arrange
            Guid orgId = Guid.NewGuid();
            var DLEventsList = EventsMockData.GetListOfDLEvents();
            _eventRepoMock.Setup(x => x.GetAllCreatedEventsByOrganisation(It.Is<Guid>(x => x == orgId))).ReturnsAsync(DLEventsList);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetAllCreatedEventsByOrganisation(orgId);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(DLEventsList.Count, result.Count);


        }
        [Fact]
        public async void GetAllCreatedEventsByOrganiser_SuccessTest()
        {
            //Arrange
            Guid organiserId = Guid.NewGuid();
            var DLEventsList = EventsMockData.GetListOfDLEvents();
            _eventRepoMock.Setup(x => x.GetAllCreatedEventsByOrganiser(It.Is<Guid>(x => x == organiserId))).ReturnsAsync(DLEventsList);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetAllCreatedEventsByOrganiser(organiserId);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(DLEventsList.Count, result.Count);


        }

        [Fact]
        public async void GetFilteredEvents_SuccessTest()
        {
            //Arrange
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            decimal startPrice = 300;
            decimal endPrice = 400;
            string location = "Hyderabad";
            string name = "Techspace";
            bool isFree = true;
            List<int> categoryIds = new List<int> { 1, 2, 3, 4, 5 };
            int pageNumber = 1;
            int pageSize = 10;

            var DLEventsList = EventsMockData.GetListOfDLEvents();
            _eventRepoMock.Setup(x => x.GetFilteredEvents(It.Is<DateTime>(x => x == startDate), It.Is<DateTime>(x => x == endDate), It.Is<decimal>(x => x == startPrice), It.Is<decimal>(x => x == endPrice), It.Is<string>(x => x == location),
                It.Is<string>(x => x == name), It.Is<bool>(x => x == isFree), It.Is<List<int>>(x => x == categoryIds), It.Is<int>(x => x == pageNumber), It.Is<int>(x => x == pageSize))).ReturnsAsync(DLEventsList);

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.GetFilteredEvents(startDate, endDate, startPrice, endPrice, location, name, isFree, categoryIds, pageNumber, pageSize);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(DLEventsList.Count, result.Count);


        }

        [Fact]
        public async void SoftDelete_SuccessTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            Guid updatedBy = Guid.NewGuid();
            DateTime updateOn = DateTime.Now;

            _eventRepoMock.Setup(x => x.UpdateIsActive(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updateOn))).ReturnsAsync((true, "Event Deleted Successfully"));

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.SoftDelete(eventId, updatedBy, updateOn);

            //Assert
            Assert.True(result.isActiveUpdated);
            Assert.Equal("Event Deleted Successfully", result.message);


        }

        [Fact]
        public async void SoftDelete_FailureTest()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            Guid updatedBy = Guid.NewGuid();
            DateTime updateOn = DateTime.Now;

            _eventRepoMock.Setup(x => x.UpdateIsActive(It.Is<Guid>(x => x == eventId), It.Is<Guid>(x => x == updatedBy), It.Is<DateTime>(x => x == updateOn))).ReturnsAsync((false, "Event Not Found"));

            var eventServices = new EventServices(_eventRepoMock.Object, null, null);

            //Act
            var result = await eventServices.SoftDelete(eventId, updatedBy, updateOn);

            //Assert
            Assert.False(result.isActiveUpdated);
            Assert.Equal("Event Not Found", result.message);


        }
    }
}
