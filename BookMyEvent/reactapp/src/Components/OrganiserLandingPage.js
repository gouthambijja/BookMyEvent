import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { fetchEventsCreatedBy, fetchNoOfOrganisationEventsRequests } from '../Features/ReducerSlices/EventsSlice';
import {  fetchNoOfRequestedPeers, fetchRequestedPeers } from "../Features/ReducerSlices/OrganisersSlice";

import {  fetchMyEventsRequests, fetchOrganisationEventsRequests } from '../Features/ReducerSlices/EventsSlice';
import OrganiserEventCard from './OrganiserEventCards'
import { Link } from 'react-router-dom';

const OrganiserLandingPage = () => {
  let cnt =0;
    const dispatch = useDispatch();
    const profile = useSelector((state) => state.profile.info);
    //console.log(profile);
    let myEvents = useSelector((state) => state.events.myEvents);
    // let eventRequests = useSelector((state) => state.events.eventRequests);
    let NoOfEventRequests = useSelector((state) => state.events.noOfEventRequests);
    // const requestedPeers = useSelector(store => store.organisers.requestedOrganisers);
    const NoOfRequestedPeers = useSelector(store => store.organisers.NoOfRequestedOrganisers);

    useEffect(() => {
        if (myEvents.length === 0) {
        dispatch(fetchEventsCreatedBy(profile.administratorId));
        }
        // if(requestedPeers.length==0 && profile.roleId != 4){
        //   dispatch(fetchRequestedPeers(profile.organisationId));
        // }
        if(NoOfRequestedPeers==0 && profile.roleId != 4){
          dispatch(fetchNoOfRequestedPeers(profile.organisationId));
        }
        if(NoOfEventRequests==0 && profile.roleId != 4){
          dispatch(fetchNoOfOrganisationEventsRequests(profile.organisationId))
        }
        // if (eventRequests == 0 && profile.roleId !=4){
        //   dispatch(fetchOrganisationEventsRequests(profile.organisationId));
        // }
},[]);
  return (
    <div style={{display:'flex',justifyContent:"center",flexWrap:'wrap',gap:'20px',padding:'40px'}}>
          {myEvents.length > 0 ? myEvents.map((event) => (
              <OrganiserEventCard key = {cnt++} event={event} />
          )) : <h1>No events created yet</h1>}
    </div>
  )
}

export default OrganiserLandingPage
