import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getAdminById } from "../../Services/AdminServices";
import { getUserById } from "../../Services/UserServices";
import organiserServices from "../../Services/OrganiserServices";

export const getAdminByIdThunk = createAsyncThunk(
  "admin/setAdminProfile",
  async (id) => {
    const profile = await getAdminById(id);
    return profile;
  }
);
export const getUserByIdThunk = createAsyncThunk(
  "User/setUserProfile",
  async (id) => {
    const profile = await getUserById(id);
    return profile;
  }
);
export const getOrganiserByIdThunk = createAsyncThunk(
  "Organiser/setOrganiserProfile",
  async (id) => {
    const profile = await organiserServices.getOrganiserById(id);
    return profile;
  }
);
const ProfileInitialState = {
  info:{}
};
const profileSlice = createSlice({
  name: "profile",
  initialState: ProfileInitialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(getAdminByIdThunk.fulfilled, (state, action) => {
      state.info = action.payload;
    })
    .addCase(getUserByIdThunk.fulfilled,(state,action)=>{
      state.info = action.payload;
    })
    .addCase(getOrganiserByIdThunk.fulfilled,(state,action)=>{
      state.info = action.payload;
    })
  },
});
const { actions, reducer } = profileSlice;
export default reducer;
