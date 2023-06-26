import axios from '../Api/Axios';
import { loginAdminThunk, setAuth} from '../Features/ReducerSlices/authSlice';
import store from '../App/store';
import jwtDecode from 'jwt-decode';
import { getAdminByIdThunk ,getUserByIdThunk,getOrganiserByIdThunk} from '../Features/ReducerSlices/ProfileSlice';

const RefreshToken = () => {

    const refresh = async () => {
        const response = await axios.get('/auth/getNewAccessTokenUsingRefreshToken', {
            withCredentials: true
        });
        store.dispatch(setAuth(response?.data));
        const auth = jwtDecode(response?.data);
        console.log(auth);
        if(auth.role == "Admin")
        await store.dispatch(getAdminByIdThunk(auth.nameid)).unwrap();
        else if(["Owner","Secondary_Owner","Peer"].includes(auth.role))
        await store.dispatch(getOrganiserByIdThunk(auth.nameid)).unwrap();
        else 
        await store.dispatch(getUserByIdThunk(auth.nameid)).unwrap();
        console.log(store.getState());
        return response.data;
    }
    return refresh;
};

export default RefreshToken;