import Axios from "../Api/Axios";
import store from "../App/store";
import { getCategoryThunk } from "../Features/ReducerSlices/CategorySlice";
import { getFormFieldsThunk } from "../Features/ReducerSlices/FormFieldsSlice";
import { fetchOrganisations } from "../Features/ReducerSlices/OrganisationsSlice";
import { fetchOrganisationOrganisers } from "../Features/ReducerSlices/OrganisersSlice";


    const categoryLoader = async () => {
      if (store.getState().category.categories.length > 0) return null;
      else{
        console.log("heyyeh");
        await store.dispatch(getCategoryThunk()).unwrap();
        return null;
      }
    };
    const FormFieldsLoader = async() =>{
      
        if(store.getState().formFields.formFields.length > 0)return null;
        else{
            await store.dispatch(getFormFieldsThunk()).unwrap();
            return null;
        }
    }
    const OrganisationsLoader=async() =>{
      if(store.getState().organisations.organisations.length>0)return null;
      else{
        await store.dispatch(fetchOrganisations()).unwrap();
        return null;
      }
    }
    const OrganisationTreeLoader=async() =>{
      if(store.getState().organisers.myOrganisationOrganisers.length>0)return null;
      else{
    const profile = store.getState().profile.info;

        await store.dispatch(fetchOrganisationOrganisers(profile.organisationId)).unwrap();
        return null;
      }
    }

export default {categoryLoader,FormFieldsLoader,OrganisationsLoader,OrganisationTreeLoader};
