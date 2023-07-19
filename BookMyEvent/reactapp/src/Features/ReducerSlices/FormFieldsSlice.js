import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import FormFieldServices from "../../Services/FormFieldServices"
const InitialState = {
  formFields:[],
  fileTypes:[]
};
export const getFormFieldsThunk = createAsyncThunk(
    'FormFields/getFormFieldsActions',async() =>{
        return await FormFieldServices().getFormFields();
    }
)
export const getFileTypesThunk = createAsyncThunk(
  'FileType/GetFileTypes',async () =>{
    return await FormFieldServices().getFileTypes();
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
      .addCase(getFileTypesThunk.fulfilled,(state,action)=>{
        state.fileTypes = action.payload;
      })
  },
});
const { actions, reducer } = categorySlice;
export default reducer;
