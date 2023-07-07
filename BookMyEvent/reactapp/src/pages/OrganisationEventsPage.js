import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { fetchOrganisationEvents } from '../Features/ReducerSlices/EventsSlice';
import OrganiserEventCard from '../Components/OrganiserEventCards';
import { Link } from 'react-router-dom';

const OrganisationEventsPage = () => {
    const dispatch = useDispatch();
    const profile = useSelector((state) => state.profile.info);
    //console.log(profile);
    let myEvents = useSelector((state) => state.events.organisationEvents);
    useEffect(() => {
        if (myEvents.length === 0) {
            dispatch(fetchOrganisationEvents(profile.organisationId));
        }
    }, []);
    return (
        <div style={{ display: 'flex', justifyContent: "center", flexWrap: 'wrap', gap: '20px', padding: '40px' }}>
            {myEvents.length > 0 ? myEvents.map((event) => (
                <OrganiserEventCard event={event} />
            )) : <h1>No events created yet</h1>}
        </div>
    )
}

export default OrganisationEventsPage;
