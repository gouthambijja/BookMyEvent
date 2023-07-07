using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.BLL.Models;
namespace BookMyEvent.BLL.RequestModels
{
    public class OrganiserRegistration
    {
        public BLOrganisation? Organisation { get; set; }
        public BLAdministrator? Organiser { get; set; }
    }
}
