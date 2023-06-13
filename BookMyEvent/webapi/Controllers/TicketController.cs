using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController:ControllerBase
    {
        private readonly ITicketServices _ticketServices;
        public TicketController(ITicketServices ticketServices)
        {
            _ticketServices = ticketServices;
        }
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

        [HttpGet("getusereventtickets")]
        public async Task<IActionResult> GetAllUserEventTickets(Guid userId, Guid eventId)
        //public async Task<List<(BLTicket ticket, List<BLUserInputFormField> userDetails)>?> GetAllUserEventTickets(Guid userId, Guid eventId)
        {
            try
            {
                List<(BLTicket ticket, List<BLUserInputFormField> userDetails)> allTickets= await _ticketServices.GetUserEventTickets(userId, eventId);
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

        [HttpPut("cancelticket")]
        public async Task<IActionResult> CancelTicket(Guid ticketId)
        {
            try
            {
                bool isCancelled= await _ticketServices.CancelTicket(ticketId);
                if (!isCancelled) { return BadRequest("Error in BL"); }
                return Ok(true);

            }
            catch { return BadRequest("Error in Controller"); }
        }

    }
}
