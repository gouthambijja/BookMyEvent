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
    console.log(event);
    const [eventData, setEventData] = useState(event || {});
    const [isEditMode, setIsEditMode] = useState(false);

    //useEffect(async () => {
    //    if (!event) {
    //        var result = await eventServices.getEventById(eventId);
    //        setEventData(result || {});
    //    }
    //}, [eventId, event, dispatch]);

    useEffect(() => {
        if (!event) {
            eventServices.getEventById(eventId).then(result => {
                setEventData(result || {});
            });
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
            <Box
                component="form"
                sx={{
                    '& .MuiTextField-root': { m: 1, width: '25ch' },
                }}
                noValidate
                autoComplete="off"
            >
                {/*eventId: '00000000-0000-0000-0000-000000000000',*/}
                {/*eventName: null,*/}
                {/*startDate: '0001-01-01T00:00:00',*/}
                {/*categoryId: 0,*/}
                {/*endDate: '0001-01-01T00:00:00',*/}
                {/*capacity: 0,*/}
                {/*availableSeats: 0,*/}
                {/*profileImgBody: null,*/}
                {/*description: null,*/}
                {/*location: null,*/}
                {/*country: null,*/}
                {/*state: null,*/}
                {/*city: null,*/}
                {/*isPublished: false,*/}
                {/*isCancelled: false,*/}
                {/*maxNoOfTicketsPerTransaction: 0,*/}
                {/*createdOn: '0001-01-01T00:00:00',*/}
                {/*rejectedBy: null,*/}
                {/*rejectedOn: null,*/}
                {/*updatedBy: null,*/}
                {/*updatedOn: null,*/}
                {/*rejectedReason: null,*/}
                {/*eventStartingPrice: 0,*/}
                {/*eventEndingPrice: 0,*/}
                {/*isFree: false,*/}
                {/*isActive: null,*/}
                {/*organisationId: '00000000-0000-0000-0000-000000000000',*/}
                {/*formId: '00000000-0000-0000-0000-000000000000',*/}
                {/*registrationStatusId: 0,*/}
                {/*createdBy: '00000000-0000-0000-0000-000000000000',*/}
                {/*acceptedBy: null*/}

                <TextField id="outlined-basic" label="Event Name" variant="outlined" defaultValue={event.eventName} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Description" variant="outlined" defaultValue={event.description} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Location" variant="outlined" defaultValue={event.location} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Country" variant="outlined" defaultValue={event.country} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event State" variant="outlined" defaultValue={event.state} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event City" variant="outlined" defaultValue={event.city} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Start Date" variant="outlined" defaultValue={event.startDate} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event End Date" variant="outlined" defaultValue={event.endDate} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Capacity" variant="outlined" defaultValue={event.capacity} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Available Seats" variant="outlined" defaultValue={event.availableSeats} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Starting Price" variant="outlined" defaultValue={event.eventStartingPrice} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Ending Price" variant="outlined" defaultValue={event.eventEndingPrice} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Max No Of Tickets Per Transaction" variant="outlined" defaultValue={event.maxNoOfTicketsPerTransaction} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Category Id" variant="outlined" defaultValue={event.categoryId} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Form Id" variant="outlined" defaultValue={event.formId} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Registration Status Id" variant="outlined" defaultValue={event.registrationStatusId} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Created By" variant="outlined" defaultValue={event.createdBy} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Accepted By" variant="outlined" defaultValue={event.acceptedBy} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Rejected By" variant="outlined" defaultValue={event.rejectedBy} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Rejected On" variant="outlined" defaultValue={event.rejectedOn} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Updated By" variant="outlined" defaultValue={event.updatedBy} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Updated On" variant="outlined" defaultValue={event.updatedOn} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Rejected Reason" variant="outlined" defaultValue={event.rejectedReason} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Is Published" variant="outlined" defaultValue={event.isPublished} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Is Cancelled" variant="outlined" defaultValue={event.isCancelled} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Is Free" variant="outlined" defaultValue={event.isFree} disabled={!isEditMode} required />
                <TextField id="outlined-basic" label="Event Is Active" variant="outlined" defaultValue={event.isActive} disabled={!isEditMode} required />

            </Box>
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
