import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { fetchEventsCreatedBy, fetchMyEventsRequests, fetchOrganisationEventsRequests } from '../Features/ReducerSlices/EventsSlice';
//import OrganiserEventCard from './OrganiserEventCards'
import { Link } from 'react-router-dom';
import OrganiserEventCard from '../Components/OrganiserEventCards';

const EventRequestsPage = () => {
    const dispatch = useDispatch();
    const profile = useSelector((state) => state.profile.info);
    console.log(profile);
    let eventRequests = useSelector((state) => state.events.eventRequests);
    useEffect(() => {
        if (eventRequests.length === 0) {
            console.log("I am in useffect");
            if (profile.roleId != 4)
                dispatch(fetchOrganisationEventsRequests(profile.organisationId));
            else {
                dispatch(fetchMyEventsRequests(profile.administratorId));
            }
        }
    }, []);
    return (
        <div style={{ display: 'flex', justifyContent: "center", flexWrap: 'wrap', gap: '20px', padding: '40px' }}>
            {eventRequests.length > 0 ? eventRequests.map((event) => (
                <OrganiserEventCard event={event} />
            )) : <h1>No events requests found</h1>}
        </div>
    )
}

export default EventRequestsPage