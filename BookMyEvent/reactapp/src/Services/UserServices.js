import axios from "axios";
import Axios from "../Api/Axios";

const getUserById = async (id) => {
    const response = await Axios.get(`api/User/?id=${id}`,
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

export { loginUser, getUserById };