import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import jwtDecode from 'jwt-decode';
import { loginAdmin } from '../../Services/AdminServices';

const authInitialState = {
    accessToken:'',
    role:'',
    id:'',
}
export const loginAdminThunk = createAsyncThunk(
    'auth/loginAdminAction',
    async(formData)=>{
        const auth = await loginAdmin(formData);
        return auth;
    }
)
const authSlice = createSlice({
    name:'auth',
    initialState:authInitialState,
    reducers:{
        clearAuth(state,action){
            state.accessToken = '';
            state.role = '';
            state.id = '';
        }
        
    },
    extraReducers(builder){
        builder.
        addCase(loginAdminThunk.fulfilled,(state,action)=>{
            state.accessToken = action.payload
            const authInfo =jwtDecode(action.payload) ;
            state.role = authInfo?.role;
            state.id = authInfo?.nameid;
        })

    }
    
})
const{actions,reducer} = authSlice;
const {clearAuth} = actions;
export {clearAuth};
export default reducer;