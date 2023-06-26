using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }
        /// <summary>
        /// Service To Add a transaction
        /// </summary>
        /// <param name="bLTransaction"></param>
        /// <returns>Transaction Details</returns>
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
        [HttpGet("getAllRegisteredEvents")]
        public async Task<IActionResult> GetAllRegisteredEvents(Guid UserId)
        {
            try
            {
                return Ok(await _transactionServices.GetAllRegisteredEventsByUserId(UserId));
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Service to Get Transaction By transactionId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>Transaction Details</returns>
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
        /// <summary>
        /// Service to Get transactionsby a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List Of Transactions By A user</returns>
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
