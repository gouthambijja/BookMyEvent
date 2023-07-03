using BookMyEvent.BLL.Models;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    /// <summary>
    /// This Interface consists of methods related to a Transaction
    /// </summary>
    public interface ITransactionServices
    {
       /// <summary>
       /// Method to add a new transaction along with tickets
       /// </summary>
       /// <param name="transaction"> A tuple of Transaction Object and List of UserInputForm Objects </param>
       /// <returns> Transaction Object </returns>
        Task<BLTransaction> Add((BLTransaction transaction, List<BLUserInputForm> forms) transaction);
       
       /// <summary>
       /// Method to get all Transactions of an Event
       /// </summary>
       /// <param name="eventId"></param>
       /// <returns> List of Transactions of an Event </returns>
        Task<List<BLTransaction>> GetAllTransactionsByEventId(Guid eventId);

        /// <summary>
        /// Method to get Al Transactions made by an User 
        /// </summary>
        /// <param name="userId"> User Id </param>
        /// <returns> List of All Transactions made by an User </returns>
        Task<List<BLTransaction>> GetAllTransactionsByUserId(Guid userId);
        /// <summary>
        /// It is used to get all the registered events of a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Event>> GetAllRegisteredEventsByUserId(Guid userId);
        Task<List<Guid>> GetAllUserRegisteredEventIds(Guid userId);
        Task<int> GetNoOfTransactionsByUserId(Guid userId);
        Task<decimal> GetAmountByUserId(Guid userId);
    }
}
