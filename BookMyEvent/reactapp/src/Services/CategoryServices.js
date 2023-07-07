import AxiosPublic from "../Api/Axios";
import AxiosPrivate from "../Hooks/AxiosPrivate";

const CategoryServices = () => {
  const Axios = AxiosPrivate();
  const getCategory = async () => {
    const response = await AxiosPublic.get("api/EventCategory");
    return response.data;
  };
  const AddCategory = async (formData) => {
    const response = await Axios.post("api/EventCategory", formData, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
  };
  const EditCategory = async (formData) =>{
    const response = await Axios.put("api/EventCategory",formData,{
        headers:{"Content-Type":"application/json"},
        withCredentials:true,
    })
    return response.data;
  }
  return {
    getCategory,
    AddCategory,
    EditCategory
  };
};

export default CategoryServices;
