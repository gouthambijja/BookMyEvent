import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getCategory } from "../../Services/CategoryServices";

const InitialState = {
  categories:[]
};
export const getCategoryThunk = createAsyncThunk(
    'category/getCategoryAction',async() =>{
        return await getCategory();
    }
)
const categorySlice = createSlice({
  name: "category",
  initialState: {
    categories:[]
  },
  reducers: {
  },
  extraReducers(builder) {
    builder
      .addCase(getCategoryThunk.fulfilled, (state, action) => {
        state.categories = action.payload;
      })
  },
});
const { actions, reducer } = categorySlice;
export default reducer;
