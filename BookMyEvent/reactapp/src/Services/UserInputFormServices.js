import AxiosPrivate from "../Hooks/AxiosPrivate";

const apiBase = "/UserInputForm";

const UserInputForm = () =>{
    const Axios = AxiosPrivate();
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

return {
    submitUserInputForm,
    getUserFormsOfUserIdByEventId,
    getAllUserFormsByEventId,
};
}
export default UserInputForm;