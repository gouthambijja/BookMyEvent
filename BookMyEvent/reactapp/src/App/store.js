import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../Features/ReducerSlices/authSlice"
import isLoading from "../Features/ReducerSlices/loadingSlice"
import profileReducer from "../Features/ReducerSlices/ProfileSlice"
import categoryReducer from "../Features/ReducerSlices/CategorySlice"
import formFieldsReducer from "../Features/ReducerSlices/FormFieldsSlice"
import OrganisationReducer from "../Features/ReducerSlices/OrganisationsSlice";
let store = configureStore({
    reducer:{
        auth:authReducer,
        isLoading:isLoading,
        profile:profileReducer,
        category:categoryReducer,
        organisations:OrganisationReducer
        formFields:formFieldsReducer
    },
})

export default store;