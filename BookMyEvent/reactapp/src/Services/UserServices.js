import axios from "axios";
import Axios from "../Api/Axios";

const getUserById = async (id) => {
    const response = await Axios.get(`api/User/${id}`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;
}
const registerUser = async (formData) => {
    console.log(formData);
    const response = await Axios.post('/api/User/AddUser',
        formData,
        {
            'Content-Type': 'multipart/form-data',
            withCredentials: true
        }

    )
    return response.data;
}
const isEmailTaken= async (email) => {
    const response = await Axios.get(`api/User/isEmailExists/${email}`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;
}
const loginUser = async (formData) => {
    
    const response = await Axios.post('api/User/login',
        JSON.stringify(formData),
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }

    )
    return response.data;
}

export  {loginUser,getUserById}
export default { registerUser,isEmailTaken };