import axios from "axios";
import Axios from "../Api/Axios";

const getAdminById = async(id) =>{
    // try{
        const response = await Axios.get(`/api/Admin/AdminById?AdminId=${id}`,
            {
                headers: { 'Content-Type': 'application/json' },
                withCredentials: true
            }
            );
            return response.data;
            // console.log(jwtDecode(response.data?.AccessToken));
            // dispatch(setAuth(response?.data));
            // setPersist();
            // navigate(from);
        // }
        // catch{
        //     setErrMsg({open:true,msg:"Login Failed, please try again."});
        //     return 
        // }

}

const loginAdmin = async(formData) =>{
    const response = await Axios.post('/api/admin/loginAdmin',
        JSON.stringify(formData),
        {
            headers:{'Content-Type':'application/json'},
            withCredentials:true 
        }

    )
    return response.data;
}
export {loginAdmin,getAdminById};