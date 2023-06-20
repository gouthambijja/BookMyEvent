import { createSlice } from "@reduxjs/toolkit";

const eventData = {
  EventId: "",
  EventName: "",
  StartDate: "",
  EndDate: "",
  Capacity: "",
  Description: "",
  Country:"",
  State:"",
  City:"",
  Location: "",
  MaxNoOfTicketsPerTransaction: "",
  EventStartingPrice: "",
  EventEndingPrice: "",
  IsFree: false,
  IsActive: false,
  OrganisationId: "",
};
const EventRegistrationFormSlice = createSlice({
  name: "EventRegistrationForm",
  initialState: {
    eventData: eventData,
  },
  reducers: {
    setEventData(state,action){
        const {checked,value,name,type} = action.payload;
        state.eventData = {...state.eventData,[name]: type === "checkbox" ? checked : value};
    },
    resetEventData(state,action){
      state.eventData = eventData;
    }
  },
});
const { actions, reducer } = EventRegistrationFormSlice;
const {setEventData,resetEventData} = actions;
export {setEventData,resetEventData}
export default reducer;
