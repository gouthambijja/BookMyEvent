using System;
using System.Collections.Generic;

namespace db.Models;

public partial class RegistrationStatus
{
    public byte RegistrationStatusId { get; set; }

    public string RegStatus { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
