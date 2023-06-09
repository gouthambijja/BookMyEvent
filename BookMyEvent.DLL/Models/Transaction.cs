using System;
using System.Collections.Generic;

namespace db.Models;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid UserId { get; set; }

    public Guid EventId { get; set; }

    public decimal Amount { get; set; }

    public int NoOfTickets { get; set; }

    public DateTime? TransactionTime { get; set; }

    public bool IsSuccessful { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User User { get; set; } = null!;
}
