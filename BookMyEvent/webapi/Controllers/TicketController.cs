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
        public async Task<List<(BLTicket ticket, List<BLUserInputFormField> userDetails)>> Get(Guid transactionId)
        {
            try
            {
                return await _ticketServices.GetAllTicketsByTransactionId(transactionId);
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("getusereventtickets")]
        public async Task<List<(BLTicket ticket, List<BLUserInputFormField> userDetails)>?> GetAllUserEventTickets(Guid userId, Guid eventId)
        {
            try
            {
                return await _ticketServices.GetUserEventTickets(userId, eventId);
            }
            catch { return null; }
        }

        [HttpPut("cancelticket")]
        public async Task<bool> CancelTicket(Guid ticketId)
        {
            try
            {
                return await _ticketServices.CancelTicket(ticketId);

            }
            catch { return false; }
        }

    }
}
