import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../Features/ReducerSlices/authSlice"
import isLoading from "../Features/ReducerSlices/loadingSlice"
import profileReducer from "../Features/ReducerSlices/ProfileSlice"
import categoryReducer from "../Features/ReducerSlices/CategorySlice"
let store = configureStore({
    reducer:{
        auth:authReducer,
        isLoading:isLoading,
        profile:profileReducer,
        category:categoryReducer
    },
})

export default store;