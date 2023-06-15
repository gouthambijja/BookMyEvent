import axios from "axios";
import Axios from "../Api/Axios";

const getOrganiserById = async (id) => {
    const response = await Axios.get(`api/Organiser/${id}`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;


}

const loginOrganiser = async (formData) => {
    
    const response = await Axios.post('/api/Organiser/loginOrganiser',
        JSON.stringify(formData),
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }

    )
    return response.data.accessToken;
}

const addOrganiser = async (formData) => {
    console.log(formData);
    const response = await Axios.post('api/Organiser/CreateOrganiser',
        formData,
        {
            'Content-Type': 'multipart/form-data',
            withCredentials: true
        }

    )
    return response.data;
}
export { loginOrganiser, getOrganiserById,addOrganiser };
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

const addOwner = async (formData) => {
    console.log(formData);
    const response = await Axios.post('/api/Organiser/RegisterOwner',
        formData,
        {
            headers:{'Content-Type': 'multipart/form-data'},
            withCredentials: true
        }

    )
    return response.data;
}
export { addPeer,addOwner };
