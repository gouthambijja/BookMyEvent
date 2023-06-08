using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        EventManagementSystemTeamZealContext context;
        public TicketRepository(EventManagementSystemTeamZealContext context)
        {
            this.context = context;
        }
        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            try
            {
                if (ticket != null)
                {
                    context.Tickets.Add(ticket);
                    await context.SaveChangesAsync();
                    return ticket;
                }
                else
                {
                    return new Ticket();
                }
            }
            catch (Exception ex)
            {
                return new Ticket();
            }
        }
        public async Task<List<Ticket>> GetTicketByTransactionId(Guid transactionId)
        {
            if (!transactionId.Equals(string.Empty))
            {
                return await context.Tickets.Where(e => e.TransactionId.Equals(transactionId)).ToListAsync();
            }
            return new List<Ticket>();
        }
        public async Task<Ticket> GetTicketByTicketId(Guid TicketId)
        {
            if (!TicketId.Equals(string.Empty))
            {
                return await context.Tickets.FirstOrDefaultAsync(e => e.TicketId.Equals(TicketId));
            }
            return new Ticket();
        }
        public async Task<Ticket> UpdateIsCanceled(Guid ticketId)
        {
            var ticket = context.Tickets.FirstOrDefault(e => e.TicketId.Equals(ticketId));
            if (ticket != null)
            {
                ticket.IsCancelled = true;
                await context.SaveChangesAsync();
                return ticket;
            }
            return new Ticket();
        }
        public async Task<List<Ticket>> AddMultipleTickets(List<Ticket> ticketList)
        {
            if (ticketList.Count != 0)
            {
                await context.Tickets.AddRangeAsync(ticketList);
                return ticketList;
            }
            return new List<Ticket>();
        }
    }
}
