import axios from "axios";
import Axios from "../Api/Axios";
import AxiosPrivate from "../Hooks/AxiosPrivate";

const UserServices =  () =>{
console.log("hey");
const axiosPrivate = AxiosPrivate();
const getUserById = async (id) => {
    const response = await axiosPrivate.get(`api/User/${id}`,
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
    console.log("hey");
    const response = await Axios.post('api/User/login',
        JSON.stringify(formData),
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }

    )
    return response.data;
}

return { loginUser,isEmailTaken,registerUser,getUserById}

}
export default UserServices