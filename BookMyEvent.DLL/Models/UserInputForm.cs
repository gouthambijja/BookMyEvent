using System;
using System.Collections.Generic;

namespace db.Models;

public partial class UserInputForm
{
    public Guid UserInputFormId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? AdministratorId { get; set; }

    public Guid EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User User { get; set; } = null!;

    public virtual Administration Administration { get; set; } = null!;

    public virtual ICollection<UserInputFormField> UserInputFormFields { get; set; } = new List<UserInputFormField>();
}
