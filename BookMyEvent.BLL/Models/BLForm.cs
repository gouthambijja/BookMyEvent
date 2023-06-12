using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLForm
    {
        public Guid FormId { get; set; }

        public string FormName { get; set; } = null!;

        public Guid OrganisationId { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool? IsActive { get; set; }
    }
}
