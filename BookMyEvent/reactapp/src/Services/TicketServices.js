import Axios from "../Api/Axios";

const apiBase = "/Ticket";

const getAllTicketsByTransactionId = async (transactionId) => {
    const response = await Axios.get(`${apiBase}/getallticketsbytransactionid?transactionId=${transactionId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getAllUserEventTickets = async (userId, eventId) => {
    const response = await Axios.get(`${apiBase}/getusereventtickets?userId=${userId}&eventId=${eventId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const cancelTicket = async (ticketId) => {
    const response = await Axios.put(`${apiBase}/cancelticket?ticketId=${ticketId}`, ticketId, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

export default {
    getAllTicketsByTransactionId,
    getAllUserEventTickets,
    cancelTicket,
};

