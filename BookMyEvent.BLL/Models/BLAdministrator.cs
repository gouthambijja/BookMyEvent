using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLAdministrator
    {
        public Guid AdministratorId { get; set; }

        public string AdministratorName { get; set; } = null!;

        public string? GoogleId { get; set; }

        public string AdministratorAddress { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public Guid? AccountCredentialsId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public byte RoleId { get; set; }

        public bool? IsAccepted { get; set; }

        public byte[]? ImgBody { get; set; }

        public string? ImageName { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? AcceptedBy { get; set; }

        public Guid? RejectedBy { get; set; }

        public Guid? DeletedBy { get; set; }

        public Guid OrganisationId { get; set; }

        public bool? IsActive { get; set; }
        public string? Password { get; set; }
    }
}
