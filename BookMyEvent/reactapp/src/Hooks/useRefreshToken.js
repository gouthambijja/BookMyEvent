import { useDispatch } from 'react-redux';
import axios from '../Api/Axios';
import { loginAdminThunk} from '../Features/ReducerSlices/authSlice';

const useRefreshToken = () => {
    const dispatch = useDispatch();

    const refresh = async () => {
        const response = await axios.get('/auth/getNewAccessTokenUsingRefreshToken', {
            withCredentials: true
        });
        dispatch(loginAdminThunk(response?.data))
        return response.data;
    }
    return refresh;
};

export default useRefreshToken;