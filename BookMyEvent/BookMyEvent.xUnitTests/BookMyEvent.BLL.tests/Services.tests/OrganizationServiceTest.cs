using BookMyEvent.BLL.Contracts;
using BookMyEvent.DLL.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.MockData;
using BookMyEvent.BLL.Models;
using AutoMapper;
using db.Models;
using BookMyEvent.BLL.Services;
using Xunit;

namespace BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.Services.tests
{
    public class OrganizationServiceTest
    {
        private readonly Mock<IOrganisationRepository> _organizationrepo;
        private readonly Mock<IAdministrationRepository> _Administrationrepo;
        private readonly Mapper _mapper;
        private readonly IOrganisationServices _orgservices;
        public OrganizationServiceTest()
        {
            _organizationrepo = new Mock<IOrganisationRepository>();
            _Administrationrepo = new Mock<IAdministrationRepository>();
            _mapper = Automapper.InitializeAutomapper();
            _orgservices = new OrganisationServices(_organizationrepo.Object, _Administrationrepo.Object);
        }
        [Fact]
        public async void CreateOrganizationTest()
        {
            //Arrange
            var organization = OrganizationMockData.AddOrg();
            var addorg = _mapper.Map<Organisation>(organization);
            _organizationrepo.Setup(x => x.IsOrgNameAvailable(It.Is<string>(x => x == addorg.OrganisationId.ToString()))).ReturnsAsync(false);
            _organizationrepo.Setup(x => x.AddOrganisation(It.Is<Organisation>(x => x.OrganisationName == addorg.OrganisationName))).ReturnsAsync((addorg, true, "Organisation created successfully"));

            //Act
            var result = await _orgservices.CreateOrganisation(organization);

            //Assert
            Assert.Equal("Organisation created successfully", result.Message);
        }
        [Fact]
        public async void BlockOrganizationTest_SuccessfulBlock()
        {
            //Arrange
            Guid orgid = Guid.NewGuid();
            Guid blockerid = Guid.NewGuid();
            _organizationrepo.Setup(x => x.ChangeIsActiveToFalse(orgid)).ReturnsAsync(true);
            _Administrationrepo.Setup(x => x.UpdateAllOrganisationOrganisersIsActive(orgid, blockerid)).ReturnsAsync(true);
            //Act
            var result = await _orgservices.BlockOrganisation(orgid, blockerid);

            //Assert
            Assert.True(result.IsOrganisationBlockToggled);
            Assert.Equal("Organisation blocked successfully", result.Message);
        }
        [Fact]
        public async void BlockOrganizationTest_Failure()
        {
            //Arrange
            Guid orgid = Guid.NewGuid();
            Guid blockerid = Guid.NewGuid();
            _organizationrepo.Setup(x => x.ChangeIsActiveToFalse(orgid)).ReturnsAsync(true);
            _Administrationrepo.Setup(x => x.UpdateAllOrganisationOrganisersIsActive(orgid, blockerid)).ReturnsAsync(false);
            //Act
            var result = await _orgservices.BlockOrganisation(orgid, blockerid);

            //Assert
            Assert.False(result.IsOrganisationBlockToggled);
            Assert.Equal("Organisation blocked but organiser block unsuccessfull", result.Message);
        }
        [Fact]
        public async void BlockOrganizationTest_Failure1()
        {
            //Arrange
            Guid orgid = Guid.NewGuid();
            Guid blockerid = Guid.NewGuid();
            _organizationrepo.Setup(x => x.ChangeIsActiveToFalse(orgid)).ReturnsAsync(false);
            _Administrationrepo.Setup(x => x.UpdateAllOrganisationOrganisersIsActive(orgid, blockerid)).ReturnsAsync(true);
            //Act
            var result = await _orgservices.BlockOrganisation(orgid, blockerid);

            //Assert
            Assert.False(result.IsOrganisationBlockToggled);
            Assert.Equal("Organisation blocked/unblock unsuccessfull", result.Message);
        }
        [Fact]
        public async void GetOrganizationByIdTest()
        {
            //Arrange
            var orgid = Guid.NewGuid();
            _organizationrepo.Setup(x => x.GetOrganisationById(orgid)).ReturnsAsync(_mapper.Map<Organisation>(OrganizationMockData.AddOrg()));

            //Act
            var result = await _orgservices.GetOrganisationById(orgid);

            //Assert
            Assert.IsType<BLOrganisation>(result);
            Assert.Equal("Hyderabad", result.Location);
        }
        [Fact]
        public async void GetOrganizationByIdTest_Failure()
        {
            //Arrange
            var orgid = Guid.NewGuid();
            _organizationrepo.Setup(x => x.GetOrganisationById(orgid)).ReturnsAsync((Organisation)null);

            //Act
            var result = await _orgservices.GetOrganisationById(orgid);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void IsorganizationNameTaken()
        {
            //Arrange
            _organizationrepo.Setup(x => x.IsOrgNameAvailable(OrganizationMockData.AddOrg().OrganisationName)).ReturnsAsync(true);

            //Act
            var result = await _orgservices.IsOrganisationNameTaken(OrganizationMockData.AddOrg().OrganisationName);

            //
            Assert.True(result);
        }
        [Fact]
        public async void IsorganizationNameTaken_Failure()
        {
            //Arrange
            _organizationrepo.Setup(x => x.IsOrgNameAvailable(OrganizationMockData.AddOrg().OrganisationName)).ReturnsAsync(false);

            //Act
            var result = await _orgservices.IsOrganisationNameTaken(OrganizationMockData.AddOrg().OrganisationName);

            //
            Assert.False(result);
        }
        [Fact]
        public async void UpdateOrganizationTest()
        {
            //Arrange
            var obj = OrganizationMockData.AddOrg();
            var obj1 = _mapper.Map<Organisation>(obj);
            _organizationrepo.Setup(x => x.UpdateOrganisation(It.Is<Organisation>(org => org.OrganisationId == obj1.OrganisationId))).ReturnsAsync(_mapper.Map<Organisation>(obj));

            //Act
            var result = await _orgservices.UpdateOrganisation(obj);

            //Assert
            Assert.Equal(obj.OrganisationId, obj1.OrganisationId);
            Assert.False(result.IsActive);
        }
        [Fact]
        public async void UpdateOrganizationTest_Failure()
        {
            //Arrange
            var obj = OrganizationMockData.AddOrg();
            var obj1 = _mapper.Map<Organisation>(obj);
            _organizationrepo.Setup(x => x.UpdateOrganisation(It.Is<Organisation>(org => org.OrganisationId == obj1.OrganisationId))).ReturnsAsync((Organisation)null);

            //Act
            var result = await _orgservices.UpdateOrganisation(obj);

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async void AccepOrganizationtest()
        {
            //Arrange
            Guid orgId = Guid.NewGuid();
            _organizationrepo.Setup(x => x.ChangeIsActiveToTrue(orgId)).ReturnsAsync(true);

            //Act
            var result = await _orgservices.AcceptOrganisation(orgId);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async void AccepOrganizationtest_Failure()
        {
            //Arrange
            Guid orgId = Guid.NewGuid();
            _organizationrepo.Setup(x => x.ChangeIsActiveToTrue(orgId)).ReturnsAsync(false);

            //Act
            var result = await _orgservices.AcceptOrganisation(orgId);

            //Assert
            Assert.False(result);
        }
    }
}
