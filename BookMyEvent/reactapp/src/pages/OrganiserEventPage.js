import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { updateEvent } from '../Features/ReducerSlices/EventsSlice';
import { Button, TextField, Typography } from '@mui/material';
import eventServices from '../Services/EventServices';

const OrganiserEventPage = () => {
    const { eventId } = useParams();
    const dispatch = useDispatch();
    const event = useSelector(state => {
        return state.events.organisationEvents.find(event => event.eventId === eventId);
    });
    console.log(event);
    const [eventData, setEventData] = useState(event || {});
    const [isEditMode, setIsEditMode] = useState(false);

    useEffect(async () => {
        if (!event) {
          var result = await eventServices.getEventById(eventId);
            setEventData(result || {});
        }
    }, [eventId, event, dispatch]);


    const handleEdit = () => {
        setIsEditMode(true);
    };

    const handleSave = () => {
        dispatch(updateEvent(eventData));
        setIsEditMode(false);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEventData((prevState) => ({
            ...prevState,
            [name]: value,
        }));
    };

    if (!event) {
        return <Typography variant="h6">Loading...</Typography>;
    }

    return (
        <div>
            <Typography variant="h4">Event Details</Typography>
            <TextField
                name="eventName"
                label="Event Name"
                value={eventData.eventName}
                disabled={!isEditMode}
                onChange={handleChange}
            />
            {/* Add more TextField components for other event fields */}
            {isEditMode ? (
                <>
                    <Button variant="contained" onClick={handleSave}>
                        Save
                    </Button>
                </>
            ) : (
                <>
                    <Button variant="contained" onClick={handleEdit}>
                        Edit
                    </Button>
                </>
            )}
        </div>
    );
};

export default OrganiserEventPage;
