import Axios from "../Api/Axios";
import store from "../App/store";
import { getCategoryThunk } from "../Features/ReducerSlices/CategorySlice";
import { getFileTypesThunk, getFormFieldsThunk } from "../Features/ReducerSlices/FormFieldsSlice";
import { fetchOrganisations } from "../Features/ReducerSlices/OrganisationsSlice";
import { fetchOrganisationOrganisers } from "../Features/ReducerSlices/OrganisersSlice";
import { fetchEventsCreatedBy, fetchNoOfOrganisationEventsRequests } from '../Features/ReducerSlices/EventsSlice';
import { fetchNoOfRequestedPeers } from "../Features/ReducerSlices/OrganisersSlice";


import { fetchOrganiserForms } from "../Features/ReducerSlices/OrganiserFormsSlice"
import { getAdminByIdThunk } from "../Features/ReducerSlices/ProfileSlice";
import { IncrementHomePageNumber, fetchEvents } from "../Features/ReducerSlices/HomeEventsSlice";
    const categoryLoader = async () => {
      if (store.getState().category.categories.length > 0) return null;
      else{
        //console.log("heyyeh");
        await store.dispatch(getCategoryThunk()).unwrap();
        return null;
      }
    };
    const FormFieldsLoader = async() =>{
        if(store.getState().formFields.formFields.length > 0 && store.getState().formFields.fileTypes.length > 0 )return null;
        else{
          if(store.getState().formFields.fileTypes.length > 0 )
            await store.dispatch(getFormFieldsThunk()).unwrap();
            else if(store.getState().formFields.formFields.length > 0 ){
              await store.dispatch(getFileTypesThunk()).unwrap();
            }
            else{
              await store.dispatch(getFormFieldsThunk()).unwrap();
              await store.dispatch(getFileTypesThunk()).unwrap();
            }
            return null;
        }
    }
    let pageNumber = 1;
    const OrganisationsLoader=async() =>{
      if(store.getState().organisations.organisations.length==0 && !store.getState().organisations.organisationsEnd && store.getState().organisations.pageNumber==1){

    await store.dispatch(fetchOrganisations({ pageNumber: pageNumber, pageSize: 30 })).unwrap();
  }

  return null;

}
const OrganisationTreeLoader = async (orgId) => {
  if (store.getState().organisers.myOrganisationOrganisers.length > 0) return null;
  else {
    const profile = store.getState().profile.info;

    await store.dispatch(fetchOrganisationOrganisers(orgId)).unwrap();
    return null;
  }
}
const LandingPageEventsLoader = async () => {
  if (!store.getState().homeEvents.end && store.getState().homeEvents.events.length == 0 && store.getState().homeEvents.page == 1) {
    store.dispatch(IncrementHomePageNumber());
    //console.log("hey");
    await store.dispatch(fetchEvents({ pageNumber: 1, })).unwrap();
  }
  return null;
}

const RequestsLoader = async () => {
  const profile = store.getState().profile.info;
  const myEvents = store.getState().events.myEvents;
  const NoOfEventRequests = store.getState().events.noOfEventRequests;
  const NoOfRequestedPeers = store.getState().organisers.NoOfRequestedOrganisers;
  console.log("outside loader")
  console.log(store.getState().auth.accessToken )

  if (store.getState().auth?.accessToken!="" ) {
    console.log("inside loader")
    console.log(store.getState().auth.accessToken )
    if (myEvents.length === 0) {
      store.dispatch(fetchEventsCreatedBy(profile.administratorId));
    }

    if (NoOfRequestedPeers == 0 && profile.roleId != 4) {
      store.dispatch(fetchNoOfRequestedPeers(profile.organisationId));
    }
    if (NoOfEventRequests == 0 && profile.roleId != 4) {
      store.dispatch(fetchNoOfOrganisationEventsRequests(profile.organisationId))
    }
  }
}

export default {
  LandingPageEventsLoader, categoryLoader, FormFieldsLoader, OrganisationsLoader, OrganisationTreeLoader,
  RequestsLoader
};
