import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import CategoryServices from "../../Services/CategoryServices";

const InitialState = {
  categories: [],
};
export const getCategoryThunk = createAsyncThunk(
  "category/getCategoryAction",
  async () => {
    return await CategoryServices().getCategory();
  }
);
export const addCategoryThunk = createAsyncThunk(
  "category/addNewCategoryAction",
  async (formData) => {
    return await CategoryServices().AddCategory(formData);
  }
);
export const editCategoryThunk = createAsyncThunk(
  "category/putCategoryAction",
  async (formData) => {
    return await CategoryServices().EditCategory(formData);
  }
);
const categorySlice = createSlice({
  name: "category",
  initialState: {
    categories: [],
  },
  reducers: {},
  extraReducers(builder) {
    builder
      .addCase(getCategoryThunk.fulfilled, (state, action) => {
        state.categories = action.payload;
      })
      .addCase(addCategoryThunk.fulfilled, (state, action) => {
        state.categories = [action.payload, ...state.categories];
      })
      .addCase(editCategoryThunk.fulfilled, (state, action) => {
        state.categories = state.categories.map((e) => {
          if (e.categoryId == action.payload.categoryId) return action.payload;
          else return e;
        });
      });
  },
});
const { actions, reducer } = categorySlice;
export default reducer;
