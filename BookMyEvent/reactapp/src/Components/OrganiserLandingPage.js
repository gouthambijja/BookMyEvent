import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { fetchEventsCreatedBy } from '../Features/ReducerSlices/EventsSlice';
import OrganiserEventCard from './OrganiserEventCards'
import { Link } from 'react-router-dom';

const OrganiserLandingPage = () => {
    const dispatch = useDispatch();
    const profile = useSelector((state) => state.profile.info);
    console.log(profile);
    let myEvents = useSelector((state) => state.events.myEvents);
    useEffect(() => {
        if (myEvents.length === 0) {
        dispatch(fetchEventsCreatedBy(profile.administratorId));
        }
}, [myEvents]);
  return (
    <div style={{display:'flex',justifyContent:"center",flexWrap:'wrap',gap:'20px',padding:'40px'}}>
          {myEvents.length > 0 ? myEvents.map((event) => (
              <OrganiserEventCard event={event} />
          )) : <h1>No events created yet</h1>}
    </div>
  )
}

export default OrganiserLandingPage
