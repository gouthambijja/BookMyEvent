import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import orgServices from "../../Services/OrganiserServices"


export const fetchRequestedOwners = createAsyncThunk(
    "organisers/fetchRequestedOwners",
    async (_, { rejectWithValue }) => {
        try {
            const response = await orgServices.getAllRequestedOrganisers();
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchRequestedPeers = createAsyncThunk(
    "organisers/fetchRequestedPeers",
    async(organisationId, { rejectWithValue }) => {
        try {
            const response = await orgServices.getAllRequestedPeers(organisationId);
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
            const response = await orgServices.getOrganisationOrganisers(organisationId);
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
            const response = await orgServices.acceptOrganiser(organiser);
            return response;
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
            const response = await orgServices.rejectOrganiser(organiser);
            return response;
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
            const response = await orgServices.createOrganiser(organiser);
            return response;
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
        builder.addCase(acceptOrganiser.fulfilled, (state, { payload }) => {
            state.loading = false;
            state.error = false;
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
        builder.addCase(rejectOrganiser.fulfilled, (state, { payload }) => {
            state.loading = false;
            state.error = false;
            //need to write the code to remove the organiser from the requested
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
    },
});
