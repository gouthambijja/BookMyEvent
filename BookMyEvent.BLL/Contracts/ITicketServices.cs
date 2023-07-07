using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
   /// <summary>
   /// This interface consists of operations related to Tickets
   /// </summary>
    public interface ITicketServices
    {
        
        /// <summary>
        /// This method is to add a New Ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns> Added new Ticket </returns>
        public Task<BLTicket> AddTickect(BLTicket ticket);
        /// <summary>
        /// It is for adding Multiple tickets for a single user
        /// </summary>
        /// <param name="ticketList"></param>
        /// <returns>Return true if success else returns false.</returns>
        public Task<bool> AddManyTickets(List<BLTicket> ticketList);
        /// <summary>
        /// This method is to Get all the tickets by Transaction Id
        /// The organiser is displayed with all the Transactions of an Event and when clicked on that Transaction,
        /// tickets of that transaction is displayed using this method
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns> List of Tickets of a Transaction </returns>
        public Task<List<(BLTicket ticket,List<BLUserInputFormField> userDetails)>> GetAllTicketsByTransactionId(Guid transactionId);
        /// <summary>
        /// This method is to get all Tickets of an User By Event Id 
        /// User is displayed with Registered Events and when clicked on an Event,
        /// the Tickets of that Event made by that User is displayed by this method
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns> List of Tickets of an Event By UserId </returns>
        public Task<List<UserTicketsWithDetails>> GetUserEventTickets(Guid userId, Guid eventId);
        
        /// <summary>
        /// This method is to cancel a Ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns> 
        /// True: if cancelled successfully 
        /// False: if any exception occurred
        /// </returns>
        public Task<bool> CancelTicket(Guid ticketId);
        /// <summary>
        /// to return all the tickets of a particular event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Task<List<UserTicketsWithDetails>> GetEventTickets(Guid eventId);


    }
}
