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

        public Task<bool> Delete(Guid TransactionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BLTransaction>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BLTransaction> Update(BLTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
