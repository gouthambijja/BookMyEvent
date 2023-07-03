using AutoMapper;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using BookMyEvent.DLL.Repositories;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Services
{
    public class TransactionServices:ITransactionServices
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IEventRepository eventRepository;
        private readonly ITicketServices ticketServices;   
        private Mapper _mapper;

        public TransactionServices(ITransactionRepository transactionRepository,ITicketServices ticketServices, IEventRepository eventRepository)
        {
            this.transactionRepository = transactionRepository;
            this.ticketServices = ticketServices;
            _mapper = Automapper.InitializeAutomapper();
            this.eventRepository = eventRepository;
        }

        public async Task<BLTransaction> Add((BLTransaction transaction,List<BLUserInputForm> forms) bLTransaction)
        {
            try
            {
                var transactionDL = _mapper.Map<Transaction>(bLTransaction.transaction);
                var CurrentTransaction = _mapper.Map<BLTransaction>(await transactionRepository.AddTransaction(transactionDL));
                List<BLTicket> tickets = new List<BLTicket>(bLTransaction.forms.Count());
                foreach(var form in bLTransaction.forms)
                {
                    BLTicket ticket = new BLTicket();
                    ticket.TransactionId = CurrentTransaction.TransactionId;
                    ticket.UserInputFormId = form.UserInputFormId;
                    ticket.UpdatedOn = DateTime.Now;
                    ticket.EventId = CurrentTransaction.EventId;
                    tickets.Add(ticket);
                }
                //declared a varialbe IsSuccess to store if the tickets are added succesfully or not
                bool IsSuccess =  await ticketServices.AddManyTickets(tickets);
                if(IsSuccess)
                {
                    return CurrentTransaction;
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

        public async Task<List<Event>> GetAllRegisteredEventsByUserId(Guid userId)
        {
            try
            {
                var eventIds = await transactionRepository.GetAllDistinctEventIds(userId);
                var events = await eventRepository.GetEventsByEventIds(eventIds);
                return events;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Guid>> GetAllUserRegisteredEventIds(Guid userId)
        {
            try
            {
                var eventIds = await transactionRepository.GetAllDistinctEventIds(userId);
                return eventIds;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLTransaction>> GetAllTransactionsByEventId(Guid eventId)
        {
            try
            {
                List<Transaction> transactionsListDL = await transactionRepository.GetTransactionsByEventId(eventId);
                List<BLTransaction> transactionsListBL = new List<BLTransaction>();
                foreach(var transaction in transactionsListDL)
                {
                    BLTransaction transactionBL = _mapper.Map<BLTransaction>(transaction);
                    transactionsListBL.Add(transactionBL);
                }
                return transactionsListBL;
            }
            catch
            {
                return new List<BLTransaction>();
            }
        }


        public async Task<List<BLTransaction>> GetAllTransactionsByUserId(Guid userId)
        {
            try
            {
                List<Transaction> transactionsListDL = await transactionRepository.GetTransactionsByUserId(userId);
                List<BLTransaction> transactionsListBL = new List<BLTransaction>();
                foreach (var transaction in transactionsListDL)
                {
                    BLTransaction transactionBL = _mapper.Map<BLTransaction>(transaction);
                    transactionsListBL.Add(transactionBL);
                }
                return transactionsListBL;
            }
            catch
            {
                return new List<BLTransaction>();
            }
        }
         public async Task<int> GetNoOfTransactionsByUserId(Guid userId)
        {
            try
            {
                return await transactionRepository.GetNoOfTransactionsByUserId(userId);
            }
            catch { return 0;}
        }
        public async Task<decimal> GetAmountByUserId(Guid userId)
        {
            try
            {
                return await transactionRepository.GetTotalAmountByUserId(userId);
            }
            catch { return 0; }
        }
    }
}
