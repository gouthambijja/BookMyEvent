using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BookMyEvent.DLL.Repositories;
using db.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyEvent.xUnitTests.BookMyEvent.DLL.tests.RepositoryTest.cs
{
    public class AdministrationRepositoryTesting
    {
        //[Fact]
        //public void TestGetAdministratorById()
        //{
        //   Guid guid = Guid.NewGuid();
        //    var dbContextMock=  new Mock<EventManagementSystemTeamZealContext>();
        //    var dbSetMock= new Mock<DbSet<Administration>>();
        //    dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>())).Returns(ValueTask.FromResult(new Administration()
        //    {
        //        AdministratorId = guid,
        //        AdministratorAddress="yellampeta",
        //        AdministratorName="venu"

        //    }));
        //    dbContextMock.Setup(s=>s.Administrations).Returns(dbSetMock.Object);

        //    var adminRepo= new AdministrationRepository(dbContextMock.Object);
        //    var user=adminRepo.GetAdministratorById(guid).Result;

        //    Assert.Null(user);
        //}

        //[Fact]
        //public void TestAddAdministrator()
        //{
        //    // Arrange
        //    var administrator = new Administration
        //    {
        //        AdministratorId = Guid.NewGuid(),
        //        AdministratorAddress = "yellampeta",
        //        AdministratorName = "venu"
        //    };

        //    var dbContextMock = new Mock<EventManagementSystemTeamZealContext>();
        //    var dbSetMock = new Mock<DbSet<Administration>>();

        //    // Set up the Add method to do nothing
        //    dbSetMock.Setup(s => s.Add(It.IsAny<Administration>())).Verifiable();

        //    // Set up the Set behavior to return the dbSetMock object
        //    dbContextMock.Setup(s => s.Set<Administration>()).Returns(dbSetMock.Object);

        //    var adminRepo = new AdministrationRepository(dbContextMock.Object);

        //    // Act
        //    var result = adminRepo.AddAdministrator(administrator).Result;

        //    // Assert
        //    dbSetMock.Verify(s => s.Add(It.IsAny<Administration>()), Times.Once);
        //    dbContextMock.Verify(s => s.SaveChanges(), Times.Once);
        //}
    }
}
