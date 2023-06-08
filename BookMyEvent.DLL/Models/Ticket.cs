using System;
using System.Collections.Generic;

namespace db.Models;

public partial class Ticket
{
    public Guid TicketId { get; set; }

    public Guid TransactionId { get; set; }

    public Guid UserInputFormId { get; set; }

    public bool? IsCancelled { get; set; }

    public DateTime UpdatedOn { get; set; }

    public Guid EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;

    public virtual UserInputForm UserInputForm { get; set; } = null!;
}
