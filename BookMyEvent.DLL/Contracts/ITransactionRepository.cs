using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    //it is To Perform Operations on Transaction Table//

    public interface ITransactionRepository
    {
        /// <summary>
        /// Method to return All the Transactions associated with the user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>List of transactions with User id</returns>
        public List<Transaction> GetTransactionsByUserId(Guid UserId);

        /// <summary>
        /// Method to get userId by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns> UserID of that Transaction </returns>
        public Task<Guid> GetUserIdByTransactionId(Guid transactionId);
        /// <summary>
        /// Method used to return all the Transactions associated with the Event 
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns>Lis of Transactions with Event Id</returns>
        public List<Transaction> GetTransactionsByEventId(Guid EventId);
        /// <summary>
        /// Method for Adding Transaction by taking transaction object as input
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Newly added transaction</returns>
        public Task<Transaction> AddTransaction(Transaction transaction);
        /// <summary>
        /// It is  for  Deleting the Transaction using transaction Id
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns>Deleted Transaction</returns>
        public Task<Transaction> DeleteTransaction(Guid TransactionId);
    }
}
