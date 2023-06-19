import Axios from "../Api/Axios";

const apiBase = "/UserInputForm";

const submitUserInputForm = async (inputUserForms) => {
    const response = await Axios.post(`${apiBase}/submitform`, inputUserForms, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getUserFormsOfUserIdByEventId = async (userId, eventId) => {
    const response = await Axios.get(`${apiBase}/GetUserFormsOfUserIdByEventId/${userId}/${eventId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getAllUserFormsByEventId = async (eventId) => {
    const response = await Axios.get(`${apiBase}/GetAllUserFormsByEventId/${eventId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

export default {
    submitUserInputForm,
    getUserFormsOfUserIdByEventId,
    getAllUserFormsByEventId,
};
