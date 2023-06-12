using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using db.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController:ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

        [HttpPost("addtransaction")]
        public async Task<BLTransaction> Add([FromBody] (BLTransaction transaction, List<UserInputForm> forms) bLTransaction)
        {
            try
            {
                return await _transactionServices.Add(bLTransaction);
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("GetAllTransactionsByEventId")]
        public async Task<List<BLTransaction>> GetTransactionsByEventId(Guid eventId)
        {
            try
            {
                return await _transactionServices.GetAllTransactionsByEventId(eventId);
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("GetAllTransactionsByUserId")]
        public async Task<List<BLTransaction>> GetTransactionsByUserId(Guid userId)
        {
            try
            {
                return await _transactionServices.GetAllTransactionsByUserId(userId);
            }
            catch
            {
                return null;
            }

        }


    }
}
