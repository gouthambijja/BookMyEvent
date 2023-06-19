import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import eventServices from "../../Services/EventServices";

export const fetchOrganisationEvents = createAsyncThunk(
    "events/fetchOrganisationEvents",
    async (id, { rejectWithValue }) => {
        try {
            const response = await eventServices.getAllCreatedEventsByOrganisation(id);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchEventsCreatedBy = createAsyncThunk(
    "events/fetchEventsCreatedBy",
    async (id, { rejectWithValue }) => {
        try {
            const response = await eventServices.getAllCreatedEventsByOrganiser(id);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const createEvent = createAsyncThunk(
    "events/createEvent",
    async (newEvent, { rejectWithValue }) => {
        try {
            const response = await eventServices.addNewEvent(newEvent);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const updateEventRegistrationStatus = createAsyncThunk(
    "events/updateEventRegistrationStatus",
    async (updatedEvent, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateEventRegistrationStatus(updatedEvent);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const updateEvent = createAsyncThunk(
    "events/updateEvent",
    async (updatedEvent, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateEvent(updatedEvent);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const acceptEvent = createAsyncThunk(
    "events/acceptEvent",
    async (details, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateAcceptedBy(details.eventId, details.acceptedBy, details.updatedBy, details.updatedOn);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const rejectEvent = createAsyncThunk(
    "events/rejectEvent",
    async (details, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateRejectedBy(details.eventId, details.rejectedBy, details.updatedBy, details.updatedOn);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const publishEvent = createAsyncThunk(
    "events/publishEvent",
    async (details, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateIsPublishedEvent(details.eventId, details.updatedBy, details.updatedOn);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const cancelEvent = createAsyncThunk(
    "events/cancelEvent",
    async (details, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateIsCancelledEvent(details.eventId, details.updatedBy, details.updatedOn);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);



const eventsSlice = createSlice({
    name: "events",
    initialState: {
        myEvents: [],
        organisationEvents: [],
        loading: false,
        error: false,
        message: "",
    },
    reducers: {
        clearMessage: (state) => {
            state.message = "";
        },
        clearError: (state) => {
            state.error = false;
        },
        clearEventsSlice: (state) => {
            state.eventRequests = [];
            state.myEvents = [];
            state.organisationEvents = [];
            state.loading = false;
            state.error = false;
            state.message = "";
        },
    },

    extraReducers(builder) {
        builder.addCase(fetchOrganisationEvents.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(fetchOrganisationEvents.fulfilled, (state, action) => {
            state.loading = false;
            state.organisationEvents = action.payload;
            state.message = "Successfully fetched organisation events";
        });
        builder.addCase(fetchOrganisationEvents.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(fetchEventsCreatedBy.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(fetchEventsCreatedBy.fulfilled, (state, action) => {
            state.loading = false;
            state.myEvents = action.payload;
            state.message = "Successfully fetched events created by you";
        });
        builder.addCase(fetchEventsCreatedBy.rejected, (state, action) => {

            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(createEvent.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(createEvent.fulfilled, (state, action) => {
            state.loading = false;
            state.myEvents.push(action.payload);
            state.organisationEvents.push(action.payload);
            state.message = "Successfully created event";
        });
        builder.addCase(createEvent.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(updateEventRegistrationStatus.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(updateEventRegistrationStatus.fulfilled, (state, action) => {
            const index = state.myEvents.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.myEvents[index] = action.payload;
            };
            index = state.organisationEvents.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.organisationEvents[index] = action.payload;
            };
            state.message = "Successfully updated event registration status";
        });
        builder.addCase(updateEventRegistrationStatus.rejected, (state, action) => {

            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(updateEvent.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(updateEvent.fulfilled, (state, action) => {
            const index = state.myEvents.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.myEvents[index] = action.payload;
            };
            index = state.organisationEvents.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.organisationEvents[index] = action.payload;
            };
            state.message = "Successfully updated event";
        });
        builder.addCase(updateEvent.rejected, (state, action) => {

            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(acceptEvent.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(acceptEvent.fulfilled, (state, action) => {
            const index = state.eventRequests.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.eventRequests[index] = action.payload;
            };
            index = state.organisationEvents.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.organisationEvents[index] = action.payload;
            };
            state.message = "Successfully accepted event";
        });
        builder.addCase(acceptEvent.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(rejectEvent.pending, (state) => {

            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(rejectEvent.fulfilled, (state, action) => {
            const index = state.eventRequests.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.eventRequests.splice(index, 1);
            };
            index = state.organisationEvents.findIndex((event) => event._id === action.payload._id);
            if (index !== -1) {
                state.organisationEvents.splice(index, 1);
            };
            state.message = "Successfully rejected event";
        });
        builder.addCase(rejectEvent.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
    },
});


export const { clearMessage, clearError, clearEventsSlice } = eventsSlice.actions;

export default eventsSlice.reducer;




