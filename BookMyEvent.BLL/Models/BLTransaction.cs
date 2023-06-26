using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLTransaction
    {
        public Guid? TransactionId { get; set; }

        public Guid? UserId { get; set; }

        public Guid? EventId { get; set; }

        public decimal Amount { get; set; }

        public int NoOfTickets { get; set; }

        public DateTime? TransactionTime { get; set; }

        public bool IsSuccessful { get; set; }
    }
}
