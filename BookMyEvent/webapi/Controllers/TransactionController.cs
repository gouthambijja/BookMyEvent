using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

        [HttpPost("addtransaction")]
        public async Task<IActionResult> Add([FromBody] AddTransaction bLTransaction)
        {
            try
            {
                return Ok(await _transactionServices.Add((bLTransaction.transaction, bLTransaction.ListOfUserInputForm)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllTransactionsByEventId")]
        public async Task<IActionResult> GetTransactionsByEventId(Guid eventId)
        {
            try
            {
                return Ok(await _transactionServices.GetAllTransactionsByEventId(eventId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllTransactionsByUserId")]
        public async Task<IActionResult> GetTransactionsByUserId(Guid userId)
        {
            try
            {
                return Ok(await _transactionServices.GetAllTransactionsByUserId(userId));
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
