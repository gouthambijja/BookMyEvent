import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import eventsServices from "../../Services/EventServices";
import TransationServices from "../../Services/TransationServices";

export const fetchEvents = createAsyncThunk(
  "homeEvents/fetchEvents",
  async (filters, { rejectWithValue }) => {
    try {
      const response = await eventsServices().getFilteredEvents(filters);
      return response;
    } catch (error) {
      return rejectWithValue(error.response);
    }
  }
);
export const fetchUserInfoByUserId = createAsyncThunk(
    "homeEvents/fetchuserInfoByUserid",
    async (userId,{rejectWithValue})=>{
        try{
            const response= await TransationServices().getUserInfoByUserid(userId);
            console.log(response)
            return response;
        } catch (error) {
            return rejectWithValue(error.response);
        }
    }
)
const homeEventsSlice = createSlice({
    name: "homeEvents",
    initialState: {
        events: [],
        page: 1,
        loading: false,
        error: false,
        message: "",
        registeredEventIds:[],
        NoOfTransactions:0,
        AmountSpent:0,
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
            //console.log(state.page);
        },
        SetPageNumber:(state,action) =>{
            state.page = action.payload;
        },
        AddRegisteredEventId: (state, action) => {
      
            if(state.registeredEventIds !=0 && !state.registeredEventIds.includes(action.payload))
            state.registeredEventIds=[...state.registeredEventIds,action.payload]
        },
        UpdateNoOfTransactions:(state) =>{
            if(state.NoOfTransactions !=0)
            state.NoOfTransactions=state.NoOfTransactions+1
        },
        UpdateAmountSpent:(state,action) =>{
            if(state.AmountSpent !=0)
            state.AmountSpent=state.AmountSpent+action.payload;
        },
        UpdateAvailableSeats: (state, action) => {
            let eventsCpy = [...state.events];
            eventsCpy = eventsCpy.map((e) => {
                if (e.eventId == action.payload.eventId) {
                    e.availableSeats =
                        e.availableSeats == -1
                            ? e.capacity - action.payload.availableSeats
                            : e.availableSeats - action.payload.availableSeats;
                    return e;
                } else {
                    return e;
                }
            });
            state.events = eventsCpy;
        },

    },
    extraReducers(builder) {
        builder.addCase(fetchEvents.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchEvents.fulfilled, (state, action) => {
            if(action.payload.length == 0) state.end = true;
            else state.end=false;
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
        builder.addCase(fetchUserInfoByUserId.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchUserInfoByUserId.fulfilled, (state, action) => {
            console.log(action.payload);
            state.loading = false;
            state.AmountSpent=action.payload.amount;
            state.NoOfTransactions=action.payload.noOfTransactions;
            state.registeredEventIds=action.payload.registeredEventIds;
            state.error = false;
            state.message = "fetched successfully";
        });
        builder.addCase(fetchUserInfoByUserId.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload.message;
        });
    }
});

export default homeEventsSlice.reducer;
export const { SetPageNumber, clearHomeEvents, IncrementHomePageNumber, UpdateAvailableSeats,clearErrorMsg, AddRegisteredEventId,UpdateNoOfTransactions,UpdateAmountSpent } = homeEventsSlice.actions;