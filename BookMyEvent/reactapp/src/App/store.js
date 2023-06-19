import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../Features/ReducerSlices/authSlice"
import isLoading from "../Features/ReducerSlices/loadingSlice"
import profileReducer from "../Features/ReducerSlices/ProfileSlice"
import categoryReducer from "../Features/ReducerSlices/CategorySlice"
import formFieldsReducer from "../Features/ReducerSlices/FormFieldsSlice"
import OrganisationReducer from "../Features/ReducerSlices/OrganisationsSlice";
import organisersReducer from "../Features/ReducerSlices/OrganisersSlice";
import orgFormsReducer from "../Features/ReducerSlices/OrganiserFormsSlice";
let store = configureStore({
    reducer:{
        auth:authReducer,
        isLoading:isLoading,
        profile:profileReducer,
        category:categoryReducer,
        organisations:OrganisationReducer,
        formFields:formFieldsReducer,
        organisers:organisersReducer,
        orgForms:orgFormsReducer,
    },
})

export default store;