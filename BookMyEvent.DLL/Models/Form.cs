using System;
using System.Collections.Generic;

namespace db.Models;

public partial class Form
{
    public Guid FormId { get; set; }

    public string FormName { get; set; } = null!;

    public Guid OrganisationId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool? IsActive { get; set; }

    public virtual Administration CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual ICollection<RegistrationFormField> RegistrationFormFields { get; set; } = new List<RegistrationFormField>();
}
