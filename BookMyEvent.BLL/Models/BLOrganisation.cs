using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLOrganisation
    {
        public Guid OrganisationId { get; set; }

        public string OrganisationName { get; set; } = null!;

        public string OrganisationDescription { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateTime? CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
