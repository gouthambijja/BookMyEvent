import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import FormFieldServices from "../../Services/FormFieldServices"
const InitialState = {
  formFields:[]
};
export const getFormFieldsThunk = createAsyncThunk(
    'FormFields/getFormFieldsActions',async() =>{
        return await FormFieldServices().getFormFields();
    }
)
const categorySlice = createSlice({
  name: "formFields",
  initialState:InitialState,
  reducers: {
  },
  extraReducers(builder) {
    builder
      .addCase(getFormFieldsThunk.fulfilled, (state, action) => {
        state.formFields = action.payload;
      })
  },
});
const { actions, reducer } = categorySlice;
export default reducer;
