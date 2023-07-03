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
export const fetchUserRegisteredEventIds = createAsyncThunk(
    "homeEvents/RegisteredEventIds",
    async (userId,{rejectWithValue})=>{
        try{
            const response= await TransationServices().getAllRegisteredEventIdsByUserid(userId);
            return response;
        } catch (error) {
            return rejectWithValue(error.response);
        }
    }
)
export const fetchNoOfRegisteredTransactions = createAsyncThunk(
    "homeEvents/getNoOfTransactionsByUserid",
    async (userId,{rejectWithValue})=>{
        try{
            const response= await TransationServices().getNoOfTransactionsByUserid(userId);
            return response;
        } catch (error) {
            return rejectWithValue(error.response);
        }
    }
)

export const fetchAmountByUserId = createAsyncThunk(
    "homeEvents/getAmountByUserid",
    async (userId,{rejectWithValue})=>{
        try{
            const response= await TransationServices().getAmountByUserid(userId);
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
            console.log(state.page);
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
        builder.addCase(fetchUserRegisteredEventIds.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchUserRegisteredEventIds.fulfilled, (state, action) => {
            
            state.loading = false;
            state.registeredEventIds=action.payload;
            state.error = false;
            state.message = "fetched successfully";
        });
        builder.addCase(fetchUserRegisteredEventIds.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload.message;
        });
        builder.addCase(fetchNoOfRegisteredTransactions.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchNoOfRegisteredTransactions.fulfilled, (state, action) => {
            
            state.loading = false;
            state.NoOfTransactions=action.payload;
            state.error = false;
            state.message = "fetched successfully";
        });
        builder.addCase(fetchNoOfRegisteredTransactions.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload.message;
        });
        builder.addCase(fetchAmountByUserId.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchAmountByUserId.fulfilled, (state, action) => {
            
            state.loading = false;
            state.AmountSpent=action.payload;
            state.error = false;
            state.message = "fetched successfully";
        });
        builder.addCase(fetchAmountByUserId.rejected, (state, action) => {
            state.loading = false;
            state.error = true;
            state.message = action.payload.message;
        });
    }
});

export default homeEventsSlice.reducer;
export const {SetPageNumber, clearHomeEvents,IncrementHomePageNumber, clearErrorMsg, AddRegisteredEventId,UpdateNoOfTransactions,UpdateAmountSpent } = homeEventsSlice.actions;