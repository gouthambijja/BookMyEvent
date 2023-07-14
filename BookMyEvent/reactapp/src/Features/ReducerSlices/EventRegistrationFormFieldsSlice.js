import { createSlice } from "@reduxjs/toolkit";

const initial = {
  inputFields: [
    {
      FieldType: "Select",
      Label: "Ticket Prices",
      Validations: { min: "", max: "" },
      Options: [1000],
      IsRequired: false,
    },
  ],
  isFree: false,
};

const EventRegistrationFormSlice = createSlice({
  name: "EventRegistrationFormFields",
  initialState: initial,
  reducers: {
    setInputFields(state, action) {
      state.inputFields = action.payload;
    },
    clearFormFieldsForm(state, action) {
      state = initial;
    },
    setIsFree(state, action) {
      state.isFree = true;
    },
    unsetIsFree(state, action) {
      state.isFree = false;
    },
  },
});
const { actions, reducer } = EventRegistrationFormSlice;
const { setInputFields, clearFormFieldsForm, setIsFree, unsetIsFree } = actions;
export { setInputFields, clearFormFieldsForm, setIsFree, unsetIsFree };
export default reducer;
