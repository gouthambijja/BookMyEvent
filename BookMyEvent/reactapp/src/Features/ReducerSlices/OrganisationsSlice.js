import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import orgServices from "../../Services/OrganisationService";



export const fetchOrganisations = createAsyncThunk(
    "organisations/fetchOrganisations",
    async (paginationDetails, { rejectWithValue }) => {
        try {
            const response = await orgServices().getAllOrganisations(paginationDetails.pageNumber, paginationDetails.pageSize);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchOrganisationById = createAsyncThunk(
    "organisations/fetchOrganisationById",
    async (id, { rejectWithValue }) => {
        try {
            const response = await orgServices().getOrganisationById(id);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const updateOrganisation = createAsyncThunk(
    "organisations/updateOrganisation",
    async (updatedOrg, { rejectWithValue }) => {
        try {
            const response = await orgServices().updateOrganisation(updatedOrg);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const deleteOrganisation = createAsyncThunk(
    "organisations/deleteOrganisation",
    async (id, { rejectWithValue }) => {
        try {
            const response = await orgServices().deleteOrganisation(id);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);


const organisationSlice = createSlice({
    name: "organisations",
    initialState: {
        organisations: [],
        loading: false,
        error: false,
        message: null,
        pageNumber: 1,
        pageSize: 10,
        totalOrganisations: null,
        organisationsEnd:false,
    },
    reducers: {
        clearError: (state) => {
            state.error = false;
        },
        clearSuccessMessage: (state) => {
            state.message = null;
        },
        clearOrganisationsState: (state) => {
            state.organisations = [];
            state.loading = false;
            state.error = false;
            state.pageNumber = 0;
            state.pageSize = 0;
            state.message = null;
            state.organisationsEnd=false;
        },
    },
    extraReducers(builder) {
        builder.addCase(fetchOrganisations.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchOrganisations.fulfilled, (state, action) => {
            if(action.payload.length == 0) state.organisationsEnd = true;
            state.loading = false;
            state.error = false;
            state.pageNumber++;
            state.pageSize = 10; 
            state.organisations =action.payload.organisations;
            state.totalOrganisations = action.payload.total;
            state.message = "Fetched organisations successfully";
        });
        builder.addCase(fetchOrganisations.rejected, (state, action) => {
            state.loading = false;
            state.error = true
            state.message = action.payload;
        });
        builder.addCase(fetchOrganisationById.pending, (state) => {
            state.loading = true;

        });
        builder.addCase(fetchOrganisationById.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            state.organisations = action.payload;
            state.message = "Fetched organisation successfully";
        });
        builder.addCase(fetchOrganisationById.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(updateOrganisation.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(updateOrganisation.fulfilled, (state, action) => {
            state.loading = false;
            state.message = "Updated organisation successfully";
            state.organisations.forEach(organisation => {
                if (organisation.id === action.payload.id) {
                    organisation = action.payload;
                }
            });
            state.error = false;
        });
        builder.addCase(updateOrganisation.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(deleteOrganisation.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(deleteOrganisation.fulfilled, (state, action) => {
            state.loading = false;
            state.message = action.payload;
            state.error = false;
            state.organisations = state.organisations.filter(organisation => organisation.id !== action.payload.id);
        });
        builder.addCase(deleteOrganisation.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
    },
});


export default organisationSlice.reducer;
export const { clearError, clearSuccessMessage, clearState } = organisationSlice.actions;
