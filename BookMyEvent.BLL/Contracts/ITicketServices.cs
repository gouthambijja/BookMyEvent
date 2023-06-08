using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface ITicketServices
    {
        public Task<BLTicket> AddTickect(BLTicket ticket);
        public Task<List<(BLTicket ticket,List<BLUserInputFormField> userDetails)>> GetAllEventTickets(Guid eventId);
        public Task<List<(BLTicket ticket, List<BLUserInputFormField> userDetails)>> GetUserEventTickets(Guid userId, Guid eventId);
        public Task<bool> CancelTicket(Guid ticketId);

    }
}
