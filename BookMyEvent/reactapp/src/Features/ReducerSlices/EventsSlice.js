import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import eventServices from "../../Services/EventServices";
import { getProfileUserId } from "./ProfileSlice";

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
    async (event, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateAcceptedBy(event);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const rejectEvent = createAsyncThunk(
    "events/rejectEvent",
    async (event, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateRejectedBy(event);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const publishEvent = createAsyncThunk(
    "events/publishEvent",
    async (event, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateIsPublishedEvent(event);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const cancelEvent = createAsyncThunk(
    "events/cancelEvent",
    async (event, { rejectWithValue }) => {
        try {
            const response = await eventServices.updateIsCancelledEvent(event);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const deleteEvent = createAsyncThunk(
    "events/deleteEvent",
    async (details, { rejectWithValue }) => {
        try {
            const response = await eventServices.deleteEvent(details.eventId, details.updatedBy);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchOrganisationPastEvents = createAsyncThunk(
    "events/fetchOrganisationPastEvents",
    async (data, { rejectWithValue }) => {
        try {
            const response = await eventServices.getOrganisationPastEvents(data.organisationId, data.pageNumber, data.pageSize);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchOrganiserPastEvents = createAsyncThunk(
    "events/fetchOrganiserPastEvents",
    async (data, { rejectWithValue }) => {
        try {
            console.log("i am in fetchOrganiserPastEvents ");
            console.log(data);
            const response = await eventServices.getOrganiserPastEvents(data.organiserId, data.pageNumber, data.pageSize);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const fetchOrganisationEventsRequests = createAsyncThunk(
    "events/fetchOrganisationEventsRequests",
    async (id, { rejectWithValue }) => {
        try {
            const response = await eventServices.getOrganisationEventRequests(id);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);


export const fetchMyEventsRequests = createAsyncThunk(
    "events/fetchMyEventsRequests",
    async (id, { rejectWithValue }) => {
        try {
            const response = await eventServices.getOrganiserEventRequests(id);
            return response;
        }
        catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);



let eventsSlice = createSlice({
    name: "events",
    initialState: {
        myEvents: [],
        organisationEvents: [],
        eventRequests: [],
        myPastEvents: [],
        organisationPastEvents: [],
        isMyPastEventsFetched: false,
        myPastEventsPageNo: 1,
        organisationPastEventsPageNo: 1,
        isOrgPastEventsFetched: false,
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
            state.myPastEvents = [];
            state.organisationPastEvents = [];
            state.myPastEventsPageNo = 1;
            state.organisationPastEventsPageNo = 1;
            state.isMyPastEventsFetched = false;
            state.isOrgPastEventsFetched = false;
            state.loading = false;
            state.error = false;
            state.message = "";
        },
        updateEventsSlice: (state, { event }) => {
            var isSamePerson = event.createdBy === getProfileUserId();
            if (!event.isActive) {
                state.myEvents = state.myEvents.filter((e) => e.eventId !== event.eventId);
                state.organisationEvents = state.organisationEvents.filter((e) => e.eventId !== event.eventId);
                state.eventRequests = state.eventRequests.filter((e) => e.eventId !== event.eventId);
            }
            if (event.isCancelled) {
                let index = state.myEvents.findIndex((e) => e.eventId === event.eventId);
                if (index !== -1) {
                    state.myEvents.splice(index, 1);
                    state.myPastEventsPageNo = 0;
                }
                index = state.organisationEvents.findIndex((e) => e.eventId === event.eventId);
                if (index !== -1) {
                    state.organisationEvents.splice(index, 1);
                    state.organisationPastEventsPageNo = 0;
                }
            }
            if (event.endDate < new Date()) {
                let index = state.myEvents.findIndex((e) => e.eventId === event.eventId);
                if (index !== -1) {
                    state.myEvents.splice(index, 1);
                }
                index = state.organisationEvents.findIndex((e) => e.eventId === event.eventId);
                if (index !== -1) {
                    state.organisationEvents.splice(index, 1);
                }
                state.myPastEventsPageNo = 0;
                state.organisationPastEventsPageNo = 0;
            }
            if (event.acceptedBy !== null && isSamePerson) {
                state.eventRequests = state.eventRequests.filter((e) => e.eventId !== event.eventId);
                let index = state.myEvents.findIndex((e) => e.eventId === event.eventId);
                if (index !== -1) {
                    state.myEvents[index] = event;
                }
                else {
                    state.myEvents.push(event);
                }
                index = state.organisationEvents.findIndex((e) => e.eventId === event.eventId);
                if (index !== -1) {
                    state.organisationEvents[index] = event;
                }
                else {
                    state.organisationEvents.push(event);
                }
            }
            if (event.rejectedBy !== null) {
                state.eventRequests = state.eventRequests.filter((e) => e.eventId !== event.eventId);
                if (isSamePerson) {
                    state.myEvents = state.myEvents.filter((e) => e.eventId !== event.eventId);
                    state.myPastEventsPageNo = 0;
                }
                state.organisationEvents = state.organisationEvents.filter((e) => e.eventId !== event.eventId);
                state.organisationPastEventsPageNo = 0;
            }
            if (event.rejectedBy === null && event.acceptedBy === null && event.isActive) {
                let index = state.eventRequests.findIndex((e) => e.eventId === event.eventId);
                if (index !== -1) {
                    state.eventRequests[index] = event;
                }
                else {
                    state.eventRequests.push(event);
                }
            }

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
            if (action.payload.acceptedBy === null) {
                state.eventRequests = state.eventRequests.push(action.payload);
            }
            else {
                state.myEvents.push(action.payload);
                state.organisationEvents.push(action.payload);
            }
            state.message = "Successfully created event";
        }
        );
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

            let index = state.myEvents.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.myEvents[index] = action.payload;
            };
            index = state.organisationEvents.findIndex((event) => event.eventId === action.payload.eventId);
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
            let index = state.myEvents.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.myEvents[index] = action.payload;
            };
            index = state.organisationEvents.findIndex((event) => event.eventId === action.payload.eventId);
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
            let index = state.eventRequests.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.eventRequests[index] = action.payload;
            };
            index = state.organisationEvents.findIndex((event) => event.eventId === action.payload.eventId);
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
            let index = state.eventRequests.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.eventRequests.splice(index, 1);
            };
            index = state.organisationEvents.findIndex((event) => event.eventId === action.payload.eventId);
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

        builder.addCase(deleteEvent.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(deleteEvent.fulfilled, (state, action) => {
            let index = state.myEvents.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.myEvents.splice(index, 1);
            };
            index = state.organisationEvents.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.organisationEvents.splice(index, 1);
            };
            state.message = "Successfully deleted event";
        });
        builder.addCase(deleteEvent.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(cancelEvent.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(cancelEvent.fulfilled, (state, action) => {
            let index = state.myEvents.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.myEvents[index] = action.payload;
            };
            index = state.organisationEvents.findIndex((event) => event.eventId === action.payload.eventId);
            if (index !== -1) {
                state.organisationEvents[index] = action.payload;
            };
            state.message = "Successfully cancelled event";
        });
        builder.addCase(cancelEvent.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(fetchOrganisationEventsRequests.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(fetchOrganisationEventsRequests.fulfilled, (state, action) => {
            state.loading = true;
            state.error = false;
            state.message = "Successfully fetched organisation requests";
            state.isOrgPastEventsFetched = true;
            state.eventRequests = action.payload;
        });
        builder.addCase(fetchOrganisationEventsRequests.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(fetchMyEventsRequests.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(fetchMyEventsRequests.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            state.message = "Successfully fetched my requests";
            state.isMyPastEventsFetched = true;
            state.eventRequests = action.payload;
        });
        builder.addCase(fetchMyEventsRequests.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(fetchOrganisationPastEvents.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(fetchOrganisationPastEvents.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            state.message = "Successfully fetched organisation past events";
            state.organisationPastEvents = action.payload;
            state.organisationPastEventsPageNo = state.organisationPastEventsPageNo + 1;

        });
        builder.addCase(fetchOrganisationPastEvents.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
        builder.addCase(fetchOrganiserPastEvents.pending, (state) => {
            state.loading = true;
            state.error = false;
            state.message = "";
        });
        builder.addCase(fetchOrganiserPastEvents.fulfilled, (state, action) => {
            state.loading = false;
            state.error = false;
            state.message = "Successfully fetched my past events";
            state.myPastEvents = action.payload;
            state.myPastEventsPageNo = state.myPastEventsPageNo + 1;
        });
        builder.addCase(fetchOrganiserPastEvents.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload;
        });
    },
});


export const { clearMessage, clearError, clearEventsSlice } = eventsSlice.actions;

export default eventsSlice.reducer;




