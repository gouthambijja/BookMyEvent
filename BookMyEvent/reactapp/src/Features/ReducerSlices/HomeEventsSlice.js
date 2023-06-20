import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import eventsServices from "../../Services/EventServices";

export const fetchEvents = createAsyncThunk(
    "homeEvents/fetchEvents",
    async (filters, { rejectWithValue }) => {
        try {
            const response = await eventsServices.getFilteredEvents(filters);
            return response.data;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

const homeEventsSlice = createSlice({
    name: "homeEvents",
    initialState: {
        events: [],
        page: 1,
        loading: false,
        error: false,
        message: ""
    },
    reducers: {
        clearHomeEvents: (state) => {
            state.events = [];
            state.loading = false;
            state.page = 1;
            state.error = false;
            state.message = "";
        },
        clearErrorMsg: (state) => {
            state.error = false;
            state.message = "";
        }

    },
    extraReducers(builder) {
        builder.addCase(fetchEvents.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchEvents.fulfilled, (state, action) => {
            state.loading = false;
            state.events = action.payload;
            state.error = false;
            state.page = state.page++;
            state.message = "fetched successfully";
        });
        builder.addCase(fetchEvents.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload.message;
        });
    }
});

export default homeEventsSlice.reducer;
export const { clearHomeEvents, clearErrorMsg } = homeEventsSlice.actions;