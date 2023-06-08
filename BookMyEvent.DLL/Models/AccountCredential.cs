using System;
using System.Collections.Generic;

namespace db.Models;

public partial class AccountCredential
{
    public Guid AccountCredentialsId { get; set; }

    public string Password { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public virtual ICollection<Administration> Administrations { get; set; } = new List<Administration>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
