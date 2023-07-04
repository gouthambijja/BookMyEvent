import AxiosPrivate from "../Hooks/AxiosPrivate";

const apiBase = "/Ticket";

const TicketServices = () => {
  const Axios = AxiosPrivate();
  const getAllTicketsByTransactionId = async (transactionId) => {
    const response = await Axios.get(
      `${apiBase}/getallticketsbytransactionid?transactionId=${transactionId}`,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return response.data;
  };

  const getAllUserEventTickets = async (userId, eventId) => {
    const response = await Axios.get(
      `${apiBase}/getusereventtickets?userId=${userId}&eventId=${eventId}`,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return response.data;
  };

  const cancelTicket = async (ticketId) => {
    const response = await Axios.put(
      `${apiBase}/cancelticket?ticketId=${ticketId}`,
      ticketId,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return response.data;
  };

  const getEventTickets = async (eventId) => {
    //console.log(eventId);
    const response = await Axios.get(`${apiBase}/getEventTickets?eventId=${eventId}`,
    {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
  }
  return {
    getAllTicketsByTransactionId,
    getAllUserEventTickets,
    getEventTickets,
    cancelTicket,
  };
};
export default TicketServices;
