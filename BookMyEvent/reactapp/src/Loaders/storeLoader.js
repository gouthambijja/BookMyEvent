import Axios from "../Api/Axios";
import store from "../App/store";
import { getCategoryThunk } from "../Features/ReducerSlices/CategorySlice";
import { getFormFieldsThunk } from "../Features/ReducerSlices/FormFieldsSlice";


    const categoryLoader = async () => {
      if (store.getState().category.length > 0) return null;
      else{
        console.log("heyyeh");
        await store.dispatch(getCategoryThunk()).unwrap();
        return null;
      }
    };
    const FormFieldsLoader = async() =>{
        if(store.getState().formFields.length > 0)return null;
        else{
            await store.dispatch(getFormFieldsThunk()).unwrap();
            return null;
        }
    }
    

export default {categoryLoader,FormFieldsLoader};
