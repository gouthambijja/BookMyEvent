using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLUser
    {
        public Guid UserId { get; set; }
        public string GoogleId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Guid? AccountCredentialsId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
