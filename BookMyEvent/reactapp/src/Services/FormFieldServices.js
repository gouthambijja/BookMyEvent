import Axios from "../Api/Axios"

const getFormFields = async() => {
    const response = await Axios.get("/api/OrganiserForm/FieldTypes");
    return response.data;
}

export default {getFormFields}