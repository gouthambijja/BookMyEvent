using System;
using System.Collections.Generic;

namespace db.Models;

public partial class Organisation
{
    public Guid OrganisationId { get; set; }

    public string OrganisationName { get; set; } = null!;

    public string OrganisationDescription { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public bool IsActive { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<Administration> Administrations { get; set; } = new List<Administration>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
