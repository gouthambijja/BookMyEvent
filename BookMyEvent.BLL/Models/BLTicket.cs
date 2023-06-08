using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLTicket
    {
        public Guid TicketId { get; set; }

        public Guid TransactionId { get; set; }

        public Guid UserInputFormId { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Guid EventId { get; set; }
    }
}
