using System;
using System.Collections.Generic;

namespace db.Models;

public partial class Role
{
    public byte RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Administration> Administrations { get; set; } = new List<Administration>();
}
