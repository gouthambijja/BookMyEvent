//OrganiserService
import axios from "axios";
import Axios from "../Api/Axios";
const addPeer = async (formData) => {
    console.log(formData);
    const response = await Axios.post('/api/Organiser/RegisterPeer',
        formData,
        {
            headers:{'Content-Type': 'multipart/form-data'},
            withCredentials: true
        }

    )
    return response.data;
}
export { addPeer };
