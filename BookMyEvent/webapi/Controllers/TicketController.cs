using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TicketController:ControllerBase
    {
        private readonly ITicketServices _ticketServices;
        public TicketController(ITicketServices ticketServices)
        {
            _ticketServices = ticketServices;
        }
        /// <summary>
        /// Service to Get tickets by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns>List of Tickets</returns>
        [HttpGet("getallticketsbytransactionid")]
        public async Task<IActionResult> Get(Guid transactionId)
        //public async Task<List<(BLTicket ticket, List<BLUserInputFormField> userDetails)>> Get(Guid transactionId)
        {
            try
            {
                List<(BLTicket ticket, List<BLUserInputFormField> userDetails)> allTickets= await _ticketServices.GetAllTicketsByTransactionId(transactionId);
                if (allTickets == null)
                {
                    return BadRequest("error in BL");
                }
                return Ok(allTickets);
            }
            catch
            {
                return BadRequest("error in controller");
            }
        }
        /// <summary>
        /// Service to Get All UserEvent Tickets
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns>list of Tickets User Registered Event</returns>
        [HttpGet("getusereventtickets")]
        public async Task<IActionResult> GetAllUserEventTickets(Guid userId, Guid eventId)
        //public async Task<List<(BLTicket ticket, List<BLUserInputFormField> userDetails)>?> GetAllUserEventTickets(Guid userId, Guid eventId)
        {
            try
            {
                List<UserTicketsWithDetails> allTickets= await _ticketServices.GetUserEventTickets(userId, eventId);
                if (allTickets == null)
                {
                    return BadRequest("error in BL");
                }

                var json = JsonConvert.SerializeObject(allTickets);
                return Ok(json);
            }
            catch
            {
                return BadRequest("error in controller");
            }
        }
        [HttpGet("getEventTickets")]
        public async Task<IActionResult> GetAllEventTickets(Guid eventId)
        {
            try
            {
                List<UserTicketsWithDetails> allTickets = await _ticketServices.GetEventTickets( eventId);
                if (allTickets == null)
                {
                    return BadRequest("error in BL");
                }

                var json = JsonConvert.SerializeObject(allTickets);
                return Ok(json);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Cancel Ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns>true if Successful else false</returns>
        [HttpPut("cancelticket")]
        public async Task<IActionResult> CancelTicket(Guid ticketId)
        {
            try
            {
                bool isCancelled= await _ticketServices.CancelTicket(ticketId);
                if (!isCancelled) { return BadRequest("Error in BL"); }
                return Ok(true);

            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

    }
}
