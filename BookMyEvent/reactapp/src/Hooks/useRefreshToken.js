import { useDispatch } from 'react-redux';
import axios from '../Api/Axios';
import { loginAdminThunk, setAuth} from '../Features/ReducerSlices/authSlice';
import store from '../App/store';
import { getAdminByIdThunk } from '../Features/ReducerSlices/AdminSlice';

const useRefreshToken = () => {
    const dispatch = useDispatch();

    const refresh = async () => {
        const response = await axios.get('/auth/getNewAccessTokenUsingRefreshToken', {
            withCredentials: true
        });
        dispatch(setAuth(response?.data));
        await dispatch(getAdminByIdThunk(store.getState().auth.id)).unwrap();
        return response.data;
    }
    return refresh;
};

export default useRefreshToken;