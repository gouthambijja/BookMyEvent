import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import AdminServices from "../../Services/AdminServices";
import UserServices from "../../Services/UserServices";
import organiserServices from "../../Services/OrganiserServices";
import useAxiosPrivate from "../../Hooks/AxiosPrivate";

export const getAdminByIdThunk = createAsyncThunk(
  "admin/setAdminProfile",
  async (id) => {
    const profile = await AdminServices().getAdminById(id);
    return profile;
  }
);
export const getUserByIdThunk = createAsyncThunk(
  "User/setUserProfile",
  async (id) => {
    const profile = await UserServices().getUserById(id);
    return profile;
  }
);
export const getOrganiserByIdThunk = createAsyncThunk(
  "Organiser/setOrganiserProfile",
  async (id) => {
    const profile = await organiserServices().getOrganiserById(id);
    return profile;
  }
);
export const updateOrganiserThunk = createAsyncThunk(
  "Organiser/updateOrganiserProfile",
  async (organiser) => {
    const profile = await organiserServices().updateOrganiser(organiser);
    return profile;
  }
);
export const updateUserThunk = createAsyncThunk(
  "Organiser/updateUserProfile",
  async (user) => {
    const profile = await UserServices().updateUserProfile(user);
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
    // builder.addCase(updateOrganiserThunk.fulfilled, (state, action) => {
    //   state.info = action.payload;
    // })
    // .addCase(updateOrganiserThunk.fulfilled,(state,action)=>{
    //   state.info = action.payload;
    // })
    .addCase(updateOrganiserThunk.fulfilled,(state,action)=>{
      state.info = action.payload;
    })
    .addCase(updateUserThunk.fulfilled,(state,action)=>{
      state.info = action.payload;
    })
  },
});
const { actions, reducer } = profileSlice;
export const getProfileUserId = (state) => state.profile.info.administratorId;
export default reducer;
