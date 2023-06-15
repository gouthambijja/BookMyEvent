import Axios from "../Api/Axios";
import store from "../App/store";
import { getCategoryThunk } from "../Features/ReducerSlices/CategorySlice";

const categoryLoader = async () => {
  if (store.getState().category.length > 0) return null;
  else{
    console.log("heyyeh");
    await store.dispatch(getCategoryThunk()).unwrap();
    return null;
  }
};

export { categoryLoader};
