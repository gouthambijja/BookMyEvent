import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../Features/ReducerSlices/authSlice"
import isLoading from "../Features/ReducerSlices/loadingSlice"
let store = configureStore({
    reducer:{
        auth:authReducer,
        isLoading:isLoading,
    },
})

export default store;