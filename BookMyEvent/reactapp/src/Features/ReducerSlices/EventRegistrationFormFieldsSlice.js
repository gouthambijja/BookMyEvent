import { createSlice } from "@reduxjs/toolkit";

const EventRegistrationFormSlice = createSlice({
  name: "EventRegistrationFormFields",
  initialState: {
    inputFields: [{
      FieldType: "Select",
      Label: "Ticket Prices",
      Validations: {min:"",max:""},
      Options: [0],
      IsRequired: false,
    }],
  },
  reducers: {
    setInputFields(state,action){
        state.inputFields = action.payload;
    },
    clearFormFieldsForm(state,action){
      state.inputFields = [{
        FieldType: "",
        Label: "",
        Validations: {min:"",max:""},
        Options: [],
        IsRequired: false,
      }]
    }
  },
});
const { actions, reducer } = EventRegistrationFormSlice;
const {setInputFields,clearFormFieldsForm} = actions;
export {setInputFields,clearFormFieldsForm}
export default reducer;
