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
        private Mapper _mapper;

        public TransactionServices(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
            _mapper = Automapper.InitializeAutomapper();

        }

        public async Task<BLTransaction> Add(BLTransaction bLTransaction)
        {
            try
            {
                return _mapper.Map<BLTransaction>(await transactionRepository.AddTransaction(_mapper.Map<Transaction>(bLTransaction)));
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
