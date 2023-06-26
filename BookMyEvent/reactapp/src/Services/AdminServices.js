import Axios from "../Api/Axios";
import useAxiosPrivate from '../Hooks/AxiosPrivate'
import React from 'react'
import useRefreshToken from "../Hooks/RefreshToken";
import store from "../App/store";

const AdminServices = () => {
    const axiosPrivate = useAxiosPrivate();
    const getAdminById = async (id) => {
        const response = await axiosPrivate.get(`/api/Admin/AdminById?AdminId=${id}`,
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
        const response = await axiosPrivate.post('/api/admin/AddAdmin',
            formData,
            {
                'Content-Type': 'multipart/form-data',
                withCredentials: true
            }
    
        )
        return response.data;
    }
    return { loginAdmin, getAdminById,addAdmin };
}

export default AdminServices
