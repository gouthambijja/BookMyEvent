import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getAdminById } from "../../Services/AdminServices";

export const getAdminByIdThunk = createAsyncThunk(
  "admin/setAdminProfile",
  async (id) => {
    const profile = await getAdminById(id);
    return profile;
  }
);

const adminInitialState = {
  profile: {},
};
const adminSlice = createSlice({
  name: "admin",
  initialState: adminInitialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(getAdminByIdThunk.fulfilled, (state, action) => {
      state.profile = action.payload;
    });
  },
});
const { actions, reducer } = adminSlice;
const { setAdminProfile } = actions;
export { setAdminProfile};
export default reducer;
