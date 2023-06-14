import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../Features/ReducerSlices/authSlice"
import isLoading from "../Features/ReducerSlices/loadingSlice"
import adminReducer from "../Features/ReducerSlices/AdminSlice"
let store = configureStore({
    reducer:{
        auth:authReducer,
        isLoading:isLoading,
        admin:adminReducer
    },
})

export default store;