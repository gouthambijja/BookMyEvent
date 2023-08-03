using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        private readonly FileLogger _fileLogger;
        public TransactionController(ITransactionServices transactionServices, FileLogger fileLogger)
        {
            _transactionServices = transactionServices;
            _fileLogger = fileLogger;
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
                _fileLogger.AddInfoToFile("[Add] Posting NewTransaction Success");
                return Ok(await _transactionServices.Add((bLTransaction.transaction, bLTransaction.ListOfUserInputForm)));
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[Add] Posting new Transaction Exception");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllRegisteredEvents")]
        public async Task<IActionResult> GetAllRegisteredEvents(Guid UserId)
        {
            try
            {
                _fileLogger.AddInfoToFile("[GetAllRegisteredEvents] Getting All RegisteredEvents Success");
                return Ok(await _transactionServices.GetAllRegisteredEventsByUserId(UserId));
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetAllRegisteredEvents] Getting All Registered Events Badrequest");
                return BadRequest(ex.Message);
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
                _fileLogger.AddInfoToFile("[GetTransactionByEventId] Getting EventTransactions Success");
                return Ok(await _transactionServices.GetAllTransactionsByEventId(eventId));
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetTransactionsByEventId] Getting EventTransactionsByEventID Failure");
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
                _fileLogger.AddInfoToFile("[GetTransactionsByUserId] Getting TransactionsByUserId Success");
                return Ok(await _transactionServices.GetAllTransactionsByUserId(userId));
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetTransactionsByUserId] Getting TransactionsByUserId Failure");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetUserInfo/{userId}")]
        public async Task<IActionResult> GetUserInfoByUSerId(Guid userId)
        {
            try
            {
                (List<Guid>? RegisteredEventIds, int NoOfTransactions, decimal Amount) Info = await _transactionServices.GetUserInfo(userId);
                _fileLogger.AddInfoToFile("[GetUserInfoByUserId] Getting UserInfoByUserId Success");
                return Ok(new
                {
                    RegisteredEventIds = Info.RegisteredEventIds,
                    NoOfTransactions = Info.NoOfTransactions,
                    Amount = Info.Amount
                });
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetUserInfoByUserId] Getting UserInfoByUSerId Failure");
                return BadRequest(ex.Message);
            }
        }
    }
}
