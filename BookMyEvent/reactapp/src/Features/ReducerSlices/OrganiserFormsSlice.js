import {createSlice, createAsyncThunk} from "@reduxjs/toolkit";
import orgFormServices from "../../Services/OrganiserFormServices";

export const fetchOrganiserForms = createAsyncThunk(
    "organiserForms/fetchOrganiserForms",
    async (organisationId, {rejectWithValue}) => {
        try {
            const response = await orgFormServices.getAllOrganizationForms(organisationId);
            return response;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const deleteForm = createAsyncThunk(
    "organiserForms/deleteForm",
    async (formId, {rejectWithValue}) => {
        try {
            const response = await orgFormServices.deleteForm(formId);
            return response;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);  

const organisationFormsSlice = createSlice({
    name: "organiserForms",
    initialState: {
        forms: [],
        loading: false,
        error: false,
        message: null,
    },
    reducers: {
        addForm: (state,action) => {
            state.forms.push(action.payload);
        },
        clearError: (state) => {
            state.error = false;
        },
        clearMessage: (state) => {
            state.message = null;
        },
        clearState: (state) => {
            state.forms = [];
            state.loading = false;
            state.error = false;
            state.message = null;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchOrganiserForms.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchOrganiserForms.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            state.forms = action.payload;
        });
        builder.addCase(fetchOrganiserForms.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(deleteForm.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(deleteForm.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            state.message = action.payload;
        });
        builder.addCase(deleteForm.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
    }
});

export const {addForm, clearError, clearMessage, clearState} = organisationFormsSlice.actions;
export default organisationFormsSlice.reducer;
