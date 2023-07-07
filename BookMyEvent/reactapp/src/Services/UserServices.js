import Axios from "../Api/Axios";
import AxiosPrivate from "../Hooks/AxiosPrivate";

const UserServices = () => {
  console.log("hey");
  const axiosPrivate = AxiosPrivate();
  const getUserById = async (id) => {
    const response = await axiosPrivate.get(`api/User/${id}`, {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
    });
    return response.data;
  };
  const registerUser = async (formData) => {
    const response = await Axios.post("/api/User/AddUser", formData, {
      "Content-Type": "multipart/form-data",
      withCredentials: true,
    });
    return response.data;
  };
  const GoogleSignUp = async (formData) => {
    const response = await Axios.post("/api/User/GoogleSignUp", formData, {
      "Content-Type": "multipart/form-data",
      withCredentials: true,
    });
    return response.data;
  };
  const isEmailTaken = async (email) => {
    const response = await Axios.get(`api/User/isEmailExists/${email}`, {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
    });
    return response.data;
  };
  const updateUserProfile = async (User) => {
    const response = await axiosPrivate.put("api/User/UpdateUser", User, {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
    });
    return response.data;
    };
    const blockUser = async (userId, blockedBy) => {
        const response = await axiosPrivate.delete(
            `api/User/BlockUser/${userId}/${blockedBy}`,
            {
                headers: { "Content-Type": "application/json" },
                withCredentials: true,
            }
        );
        return response.data;
    };
  const loginUser = async (formData) => {
    console.log("hey");
    const response = await Axios.post(
      "api/User/login",
      formData,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return response.data;
    };
    const filteredUsers = async (filters) => {
        const response = await axiosPrivate.get(`api/User/GetFilteredUsers?name=${filters.name}&email=${filters.email}&phoneNumber=${filters.phoneNumber}&isActive=${filters.isActive}`, {
            headers: { "Content-Type": "application/json" },
            withCredentials: true,
        });
        return response.data;
    };

    return { loginUser, isEmailTaken, registerUser, getUserById, GoogleSignUp, filteredUsers, blockUser, updateUserProfile };
};
export default UserServices;
