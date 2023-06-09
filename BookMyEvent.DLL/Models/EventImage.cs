using System;
using System.Collections.Generic;

namespace db.Models;

public partial class EventImage
{
    public Guid ImgId { get; set; }

    public byte[]? ImgBody { get; set; }

    public string? ImgType { get; set; }

    public string? ImgName { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
