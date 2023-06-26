import { axiosPrivate } from "../Api/Axios";
import { clearAuth} from "../Features/ReducerSlices/authSlice";
import store from "../App/store";


const Logout = () => {
  const _Logout = async () => {
    const logout = await axiosPrivate.post("/auth/logout");
    if (logout.data) {
        store.dispatch(clearAuth());
      localStorage.clear();
     
      return true;
    } else return false;
  };
  return _Logout;
};

export default Logout;
