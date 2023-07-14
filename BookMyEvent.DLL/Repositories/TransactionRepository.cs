using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        EventManagementSystemTeamZealContext context { get; set; }
        public TransactionRepository(EventManagementSystemTeamZealContext context)
        {
            this.context = context;
        }
        public async Task<List<Transaction>> GetTransactionsByUserId(Guid UserId)
        {
            return await context.Transactions.Where(e => e.UserId.Equals(UserId)).ToListAsync();
        }
        public async Task<int> GetNoOfTransactionsByUserId(Guid UserId)
        {
            try {

            return await context.Transactions.CountAsync(e => e.UserId.Equals(UserId));

            }
            catch
            {
                return 0;
            }

        }
        public async Task<decimal> GetTotalAmountByUserId(Guid UserId)
        {
            try
            {

                return await context.Transactions.Where(t => t.UserId == UserId).SumAsync(t => t.Amount);

            }
            catch
            {
                return 0;
            }

        }
        public async Task<Guid?> GetUserIdByTransactionId(Guid transactionId)
        {
            try
            {
                Transaction? transaction = await context.Transactions.FindAsync(transactionId);
                if (transaction != null)
                {
                    Guid? userId = transaction.UserId;
                    return userId;
                }
                else { return Guid.Empty; }
            }
            catch
            {
                return Guid.Empty; // or any appropriate value indicating an error occurred
            }
        }
        public async Task<List<Transaction>> GetTransactionsByEventId(Guid EventId)
        {
            try
            {

                List<Transaction> transactions = await context.Transactions.Where(e => e.EventId.Equals(EventId) && e.IsSuccessful.Equals(true)).ToListAsync();
                return transactions;
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            try
            {
                if (transaction != null)
                {
                    context.Transactions.Add(transaction);
                    await context.SaveChangesAsync();
                    await context.Entry(transaction).GetDatabaseValuesAsync();
                    return transaction;
                }
                else
                {
                    return new Transaction();
                }
            }
            catch (Exception ex)
            {
                return new Transaction();
            }
        }
        public async Task<Transaction> DeleteTransaction(Guid TransactionId)
        {
            try
            {
                var transaction = await context.Transactions.FindAsync(TransactionId);
                if (transaction != null)
                {
                    context.Transactions.Remove(transaction);
                    context.SaveChanges();
                    return transaction;
                }
                return new Transaction();
            }
            catch (Exception e)
            {
                return new Transaction();
            }
        }

        public async Task<List<Guid>> GetAllDistinctEventIds(Guid UserId)
        {
            try
            {
                var eventIds = await context.Transactions.Where(t => t.UserId == UserId).Select(t => t.EventId).Distinct().ToListAsync();
                return eventIds;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
