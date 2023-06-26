import AxiosPrivate from "../Hooks/AxiosPrivate";

const apiBase = "/Transaction";

const TransationServices = () =>{
const Axios = AxiosPrivate();
 const addTransaction = async (transaction) => {
    const response = await Axios.post(`${apiBase}/addtransaction`, transaction, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

 const getAllTransactionsByEventId = async (eventId) => {
     const response = await Axios.get(`${apiBase}/GetAllTransactionsByEventId?eventId=${eventId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

 const getAllTransactionsByUserId = async (userId) => {
     const response = await Axios.get(`${apiBase}/GetAllTransactionsByUserId?userId=${userId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getAllRegisteredEventsByUserid = async (userId) => {
    const response = await Axios.get(`${apiBase}/getAllRegisteredEvents?UserId=${userId}`,
    {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
}
return  {
    addTransaction,
    getAllTransactionsByEventId,
    getAllTransactionsByUserId,
    getAllRegisteredEventsByUserid,
};
}
export default TransationServices;