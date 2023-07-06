import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import orgServices from "../../Services/OrganiserServices"


export const fetchRequestedOwners = createAsyncThunk(
    "organisers/fetchRequestedOwners",
    async (_, { rejectWithValue }) => {
        try {
            const response = await orgServices().getRequestedOwners();
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchRequestedPeers = createAsyncThunk(
    "organisers/fetchRequestedPeers",
    async (organisationId, { rejectWithValue }) => {
        try {
            const response = await orgServices().getRequestedPeers(organisationId);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchOrganisationOrganisers = createAsyncThunk(
    "organisers/fetchOrganisationOrganisers",
    async (organisationId, { rejectWithValue }) => {
        try {
            const response = await orgServices().getOrganisationOrganisers(organisationId);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);


export const acceptOrganiser = createAsyncThunk(
    "organisers/acceptOrganiser",
    async (organiser, { rejectWithValue }) => {
        try {
            const response = await orgServices().acceptOrganiser(organiser);
            if (response) {
                return organiser;
            }

        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);


export const rejectOrganiser = createAsyncThunk(
    "organisers/rejectOrganiser",
    async (organiser, { rejectWithValue }) => {
        try {
            const response = await orgServices().rejectOrganiser(organiser);
            return organiser;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);



export const createOrganiser = createAsyncThunk(
    "organisers/createOrganiser",
    async (organiser, { rejectWithValue }) => {
        try {
            const response = await orgServices().createOrganiser(organiser);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const blockOrganiser = createAsyncThunk(
    "organisers/blockOrganiser",
    async (details, { rejectWithValue }) => {
        try {
            const response = await orgServices().deleteOrganiser(details.id, details.deletedById);
            return details;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

const organisersSlice = createSlice({
    name: "organisers",
    initialState: {
        requestedOrganisers: [],
        myOrganisationOrganisers: [],
        loading: false,
        error: null,
        message: null,
    },
    reducers: {
        clearReqOrganisers: (state) => {
            state.requestedOrganisers = [];
        },
        clearMyOrganisers: (state) => {
            state.myOrganisationOrganisers = [];
        },
        clearError: (state) => {
            state.error = null;
        },
    },
    extraReducers(builder) {
        builder.addCase(fetchRequestedOwners.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchRequestedOwners.fulfilled, (state, { payload }) => {
            state.loading = false;
            state.error = false;
            state.requestedOrganisers = payload;
            state.message = "Organisers fetched successfully";
        });
        builder.addCase(fetchRequestedOwners.rejected, (state, { payload }) => {
            state.loading = false;
            state.error = true
            state.message = payload;
        });
        builder.addCase(fetchRequestedPeers.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchRequestedPeers.fulfilled, (state, { payload }) => {
            state.loading = false;
            state.error = false;
            state.requestedOrganisers = payload;
            state.message = "Organisers fetched successfully";
        });
        builder.addCase(fetchRequestedPeers.rejected, (state, { payload }) => {
            state.loading = false;
            state.error = true
            state.message = payload;
        });
        builder.addCase(fetchOrganisationOrganisers.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchOrganisationOrganisers.fulfilled, (state, { payload }) => {
            state.loading = false;
            state.error = false;
            state.myOrganisationOrganisers = payload;
            state.message = "Organisers fetched successfully";
        });
        builder.addCase(fetchOrganisationOrganisers.rejected, (state, { payload }) => {
            state.loading = false;
            state.error = true
            state.message = payload;
        });
        builder.addCase(acceptOrganiser.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(acceptOrganiser.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            const index = state.requestedOrganisers.findIndex((e) => e.administratorId === action.payload.administratorId);
            if (index !== -1) {
                state.requestedOrganisers.splice(index, 1);
                state.myOrganisationOrganisers.push(action.payload);
            }
            //need to write the code to remove the organiser from the requested table and move it to organisation list
            state.message = "Organiser accepted successfully";
        });
        builder.addCase(acceptOrganiser.rejected, (state, { payload }) => {
            state.loading = false;
            state.error = true
            state.message = payload;
        });
        builder.addCase(rejectOrganiser.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(rejectOrganiser.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            const index = state.requestedOrganisers.findIndex((e) => e.administratorId === action.payload.administratorId);
            if (index !== -1) {
                state.requestedOrganisers.splice(index, 1);
            }
            state.message = "Organiser rejected successfully";
        });
        builder.addCase(rejectOrganiser.rejected, (state, { payload }) => {
            state.loading = false;
            state.error = true
            state.message = payload;
        });
        builder.addCase(createOrganiser.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(createOrganiser.fulfilled, (state, { payload }) => {
            state.loading = false;
            state.error = false;
            state.myOrganisationOrganisers.push(payload);
            state.message = "Organiser created successfully";
        });
        builder.addCase(createOrganiser.rejected, (state, { payload }) => {
            state.loading = false;
            state.error = true
            state.message = payload;
        });

        builder.addCase(blockOrganiser.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(blockOrganiser.fulfilled, (state, { payload }) => {
            state.loading = false;
            state.error = false;
            const index = state.myOrganisationOrganisers.findIndex((e) => e.administratorId === payload.id);
            if (index !== -1) {
                state.myOrganisationOrganisers.splice(index, 1);
            }
            state.message = "Organiser blocked successfully";
        });
        builder.addCase(blockOrganiser.rejected, (state, { payload }) => {
            state.loading = false;
            state.error = true
            state.message = payload;
        });
    },
});


export const { clearReqOrganisers, clearMyOrganisers, clearError } = organisersSlice.actions;
export default organisersSlice.reducer;