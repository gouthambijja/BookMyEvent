import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { updateEvent } from '../Features/ReducerSlices/EventsSlice';
import {
    TextField,
    Button,
    FormControlLabel,
    Checkbox,
    Typography,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Box,
    Chip,
    ButtonBase,
} from "@mui/material";
import eventServices from '../Services/EventServices';


const OrganiserEventPage = () => {
    const { eventId } = useParams();
    const dispatch = useDispatch();
    const event = useSelector(state => {
        return state.events.myEvents.find((event) => event.eventId === eventId);
    });
    //console.log(event);
    const [eventData, setEventData] = useState(event || {});
    const [isEditMode, setIsEditMode] = useState(false);

    useEffect(() => {
        if (!event) {
            eventServices().getEventById(eventId).then(result => {
                setEventData(result || {});
            });
        }
    }, [eventId, event, dispatch]);


    const handleEdit = () => {
        setIsEditMode(true);
    };

    const handleSave = () => {
        /* dispatch(updateEvent(eventData));*/
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
            <div style={{ display: 'flex', justifyContent: 'center' }}>
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
        </div>
    );
};

export default OrganiserEventPage;
