import { useDispatch } from "react-redux";
import { axiosPrivate } from "../Api/Axios";
import { clearAuth} from "../Features/ReducerSlices/authSlice";

const useLogout = () => {
    const dispatch = useDispatch();
  const Logout = async () => {
    const logout = await axiosPrivate.post("/auth/logout");
    if (logout.data) {
        dispatch(clearAuth());
      localStorage.clear();
      return true;
    } else return false;
  };
  return Logout;
};

export default useLogout;
