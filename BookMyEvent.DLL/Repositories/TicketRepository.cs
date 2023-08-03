using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
                    return null;
                }
            }
            catch 
            {
                return null;
            }
        }
        public async Task<List<Ticket>> GetTicketByTransactionId(Guid transactionId)
        {
            if (!transactionId.Equals(string.Empty))
            {
                return await context.Tickets.Where(e => e.TransactionId.Equals(transactionId)).ToListAsync();
            }
            return null;
        }
        public async Task<Ticket> GetTicketByTicketId(Guid TicketId)
        {
            if (!TicketId.Equals(string.Empty))
            {
                return await context.Tickets.FirstOrDefaultAsync(e => e.TicketId.Equals(TicketId));
            }
            return null;
        }

        public async Task<Ticket> GetTicketByFormId(Guid FormId)
        {
            if (!FormId.Equals(string.Empty))
            {
                return await context.Tickets.FirstOrDefaultAsync(e => e.UserInputFormId.Equals(FormId));
            }
            return null;
        }
        public async Task<List<Ticket>?> GetTicketByEventId(Guid EventId)
        {
            if (!EventId.Equals(string.Empty))
            {
                return await context.Tickets.Where(e => e.EventId.Equals(EventId)).ToListAsync();
            }
            return null;
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
            return null;
        }
        //public async Task<List<Ticket>> AddMultipleTickets(List<Ticket> ticketList)
        //{
        //    if (ticketList.Count != 0)
        //    {
        //        await context.Tickets.AddRangeAsync(ticketList);
        //        return ticketList;
        //    }
        //    return new List<Ticket>();
        //}

        public async Task<bool> AddMultipleTickets(List<Ticket> ticketList)
        {
            try
            {
                await context.Tickets.AddRangeAsync(ticketList);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Ticket>> GetTicketsByFormids(List<Guid> FormIds)
        {
            try
            {
                return await context.Tickets.Where(e => FormIds.Contains(e.UserInputFormId)).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
