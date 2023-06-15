import Axios from "../Api/Axios"

const getCategory = async() =>{
    const response = await Axios.get('api/EventCategory');
    return response.data;
}

export {getCategory};