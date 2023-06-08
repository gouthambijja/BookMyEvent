using System;
using System.Collections.Generic;

namespace db.Models;

public partial class Administration
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

    public Guid? CreatedBy { get; set; }

    public Guid? AcceptedBy { get; set; }

    public Guid? RejectedBy { get; set; }

    public Guid? DeletedBy { get; set; }

    public Guid OrganisationId { get; set; }

    public Guid? BlockedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual Administration? AcceptedByNavigation { get; set; }

    public virtual AccountCredential? AccountCredentials { get; set; }

    public virtual Administration? BlockedByNavigation { get; set; }

    public virtual Administration? CreatedByNavigation { get; set; }

    public virtual Administration? DeletedByNavigation { get; set; }

    public virtual ICollection<Event> EventAcceptedByNavigations { get; set; } = new List<Event>();

    public virtual ICollection<Event> EventCreatedByNavigations { get; set; } = new List<Event>();

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();

    public virtual ICollection<Administration> InverseAcceptedByNavigation { get; set; } = new List<Administration>();

    public virtual ICollection<Administration> InverseBlockedByNavigation { get; set; } = new List<Administration>();

    public virtual ICollection<Administration> InverseCreatedByNavigation { get; set; } = new List<Administration>();

    public virtual ICollection<Administration> InverseDeletedByNavigation { get; set; } = new List<Administration>();

    public virtual ICollection<Administration> InverseRejectedByNavigation { get; set; } = new List<Administration>();

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual Administration? RejectedByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
