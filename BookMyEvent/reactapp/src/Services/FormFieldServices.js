import AxiosPrivate from "../Hooks/AxiosPrivate";

const FormFieldServices = () => {
    const Axios = AxiosPrivate();
    const getFormFields = async() => {
        const response = await Axios.get("/api/OrganiserForm/FieldTypes");
        return response.data;
    }
    return {getFormFields};
}

export default FormFieldServices;