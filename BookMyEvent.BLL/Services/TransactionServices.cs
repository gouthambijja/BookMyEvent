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
        private readonly ITicketServices ticketServices;   
        private Mapper _mapper;

        public TransactionServices(ITransactionRepository transactionRepository,ITicketServices ticketServices)
        {
            this.transactionRepository = transactionRepository;
            this.ticketServices = ticketServices;
            _mapper = Automapper.InitializeAutomapper();
        }

        public async Task<BLTransaction> Add((BLTransaction transaction,List<UserInputForm> forms) bLTransaction)
        {
            try
            {
                var CurrentTransaction = _mapper.Map<BLTransaction>(await transactionRepository.AddTransaction(_mapper.Map<Transaction>(bLTransaction)));
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

      
    }
}
