using System;
using System.Collections.Generic;

namespace db.Models;

public partial class EventCategory
{
    public byte CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
