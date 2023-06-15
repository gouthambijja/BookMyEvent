import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import jwtDecode from "jwt-decode";
import { loginAdmin } from "../../Services/AdminServices";
import { loginOrganiser } from "../../Services/OrganiserServices";
import { loginUser } from "../../Services/UserServices";

const authInitialState = {
  accessToken: "",
  role: "",
  id: "",
};
export const loginThunk = createAsyncThunk(
  "auth/loginAction",
  async (formData) => {
    if (formData[0] == "Admin") {
      return await loginAdmin(formData[1]);
    } else if (formData[0] == "Organiser") {
        return await loginOrganiser(formData[1]);
    }
    else{
        return await loginUser(formData[1]);
    }
  }
);
const authSlice = createSlice({
  name: "auth",
  initialState: authInitialState,
  reducers: {
    setAuth(state, action) {
      state.accessToken = action.payload;
      const authInfo = jwtDecode(action.payload);
      state.role = authInfo?.role;
      state.id = authInfo?.nameid;
    },
    clearAuth(state, action) {
      state.accessToken = "";
      state.role = "";
      state.id = "";
    },
  },
  extraReducers(builder) {
    builder
      .addCase(loginThunk.fulfilled, (state, action) => {
        state.accessToken = action.payload;
        const authInfo = jwtDecode(action.payload);
        state.role = authInfo?.role;
        state.id = authInfo?.nameid;
      })
  },
});
const { actions, reducer } = authSlice;
const { clearAuth, setAuth } = actions;
export { clearAuth, setAuth };
export default reducer;
