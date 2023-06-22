import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import eventsServices from "../../Services/EventServices";

export const fetchEvents = createAsyncThunk(
    "homeEvents/fetchEvents",
    async (filters, { rejectWithValue }) => {
        try {
            const response = await eventsServices.getFilteredEvents(filters);
            return response;
        } catch (error) {
            return rejectWithValue(error.response);
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
        message: "",
        end:false
    },
    reducers: {
        clearHomeEvents: (state) => {
            state.events = [];
            state.loading = false;
            state.error = false;
            state.message = "";
            state.end =false;
        },
        clearErrorMsg: (state) => {
            state.error = false;
            state.message = "";
        },
        IncrementHomePageNumber:(state,action) =>{
            state.page = state.page + 1;
            console.log(state.page);
        },
        SetPageNumber:(state,action) =>{
            state.page = action.payload;
        }

    },
    extraReducers(builder) {
        builder.addCase(fetchEvents.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchEvents.fulfilled, (state, action) => {
            if(action.payload.length == 0) state.end = true;
            state.loading = false;
            state.events = [...state.events,...action.payload];
            state.error = false;
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
export const {SetPageNumber, clearHomeEvents,IncrementHomePageNumber, clearErrorMsg } = homeEventsSlice.actions;