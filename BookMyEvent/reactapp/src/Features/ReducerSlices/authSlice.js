import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import Axios from '../../Api/Axios';

const authInitialState = {
    accessToken:'',
    role:'',
    id:'',
}
const authSlice = createSlice({
    name:'auth',
    initialState:authInitialState,
    reducers:{
        setAuth(state,action){
            state.accessToken = action.payload.accessToken;
            state.role = action.payload.role;
            state.id = action.payload.id;
        },
        
    },
    
})
const{actions,reducer} = authSlice;
const {setAuth} = actions;
export {setAuth};
export default reducer;