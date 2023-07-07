import { createSlice } from "@reduxjs/toolkit";

const loader = createSlice({
  name: "isLoading",
  initialState: false,
  reducers: {
    setLoading(state, action) {
      state = action.payload;
      return state;
    },
  },
});

const {actions,reducer} = loader;
const {setLoading} = actions;
export {setLoading};
export default reducer;
