using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    //It is used to perform Operations on  Ticket Table//
    public interface ITicketRepository
    {
        /// <summary>
        /// This Method is used to add a Ticket to the ticket table
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns>Newly Added Ticket.</returns>
        public Task<Ticket> AddTicket(Ticket ticket);
        /// <summary>
        /// It is for adding Multiple tickets for a single user
        /// </summary>
        /// <param name="ticketList"></param>
        /// <returns>Return Newly Added Multiple Tickets.</returns>
        public Task<List<Ticket>> AddMultipleTickets(List<Ticket> ticketList);
        /// <summary>
        /// it id used to get all the tickets associated with transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns>List of Tickets with transactionId</returns>
        public Task<List<Ticket>> GetTicketByTransactionId(Guid transactionId);
        /// <summary>
        /// It is to get a ticket by taking Id as input
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns>Ticket with ticketId</returns>
        public Task<Ticket> GetTicketByTicketId(Guid TicketId);
        /// <summary>
        /// It id for Cancelling the ticket by taking the ticket Id as Parameter
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns>Cancelled Ticket</returns>
        public Task<Ticket> UpdateIsCanceled(Guid ticketId);
    }
}
