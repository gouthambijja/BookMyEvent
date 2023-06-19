import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import orgServices from "../../Services/OrganisationService";



export const fetchOrganisations = createAsyncThunk(
    "organisations/fetchOrganisations",
    async (_, { rejectWithValue  }) => {
        try {
            const response = await orgServices.getAllOrganisations();
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
            const response = await orgServices.getOrganisationById(id);
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
            const response = await orgServices.updateOrganisation(updatedOrg);
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
            const response = await orgServices.deleteOrganisation(id);
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
    },
    reducers: {
        clearError: (state) => {
            state.error = false;
        },
        clearSuccessMessage: (state) => {
            state.message = null;
        },
        clearState: (state) => {
            state.organisations = [];
            state.loading = false;
            state.error = false;
            state.message = null;
        },
    },
    extraReducers(builder) {
        builder.addCase(fetchOrganisations.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchOrganisations.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            state.organisations = action.payload;
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
