import axios from "axios";
import Axios from "../Api/Axios";

const getAdminById = async (id) => {
    const response = await Axios.get(`/api/Admin/AdminById?AdminId=${id}`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;


}

const loginAdmin = async (formData) => {
    
    const response = await Axios.post('/api/admin/loginAdmin',
        JSON.stringify(formData),
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }

    )
    return response.data;
}

const addAdmin = async (formData) => {
    console.log(formData);
    const response = await Axios.post('/api/admin/AddAdmin',
        formData,
        {
            'Content-Type': 'multipart/form-data',
            withCredentials: true
        }

    )
    return response.data;
}
export { loginAdmin, getAdminById,addAdmin };