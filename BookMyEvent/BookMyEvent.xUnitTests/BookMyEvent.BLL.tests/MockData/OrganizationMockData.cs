using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.MockData
{
    public class OrganizationMockData
    {
        public static BLOrganisation AddOrg()
        {
            return new BLOrganisation
            {
                OrganisationId = Guid.NewGuid(),
                OrganisationName = "Shashank",
                OrganisationDescription = "Null",
                Location = "Hyderabad",
                CreatedOn = DateTime.Now,
                IsActive = false,
                UpdatedOn = DateTime.Now,
            };
        }
    }
}
