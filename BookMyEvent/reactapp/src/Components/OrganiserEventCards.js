import * as React from 'react';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { deleteEvent, updateEvent } from '../Features/ReducerSlices/EventsSlice';
import { Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Typography } from '@mui/material';
import { DeleteOutline, EditOutlined, PublishOutlined } from '@material-ui/icons';
import ConfirmationDialog from './ConfirmationDialog';
import ReusableAlert from './ReusableAlert';
import { Link, useNavigate } from 'react-router-dom';
import store from '../App/store';

const OrganiserEventCard = ({ event }) => {
    const state = store.getState();
    const [openDialog, setOpenDialog] = useState(false);
    const [showAlert, setShowAlert] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');
    const [alertSeverity, setAlertSeverity] = useState('success');
    const dispatch = useDispatch();
    const profile = useSelector((state) => state.profile.info);
    const navigate = useNavigate();

    const handleConfirm = async () => {
        setOpenDialog(false);
        const tempEvent = { ...event };
        tempEvent.updatedBy = profile.administratorId;
        await dispatch(deleteEvent(tempEvent)).unwrap();
        setAlertMessage(state.events.message);
        setAlertSeverity(state.events.error ? 'error' : 'success');
        setShowAlert(true);
    };

    const handleDeleteOperation = () => {
        setOpenDialog(true);
    };

    const handleCancel = () => {
        setOpenDialog(false);
    };

    const handlePublish = () => {
        setOpenDialog(true);
    };

    const handlePublishConfirm = () => {
        setOpenDialog(false);
        const tempEvent = { ...event };
        tempEvent.isPublished = true;
        dispatch(updateEvent(tempEvent));
    };

    const handleRegistrationStatusChange = (e) => {
        const tempEvent = { ...event };
        tempEvent.registrationStatus = parseInt(e.target.value);
        dispatch(updateEvent(tempEvent));
    };

    const handleClick = () => {
        navigate(`event/${event.eventId}`);
    };

    return (
        <>
            <Card sx={{ width: '400px', boxShadow: '0px 0px 9px #d0d0d0' }}>
                <CardActionArea onClick={handleClick}>
                    <CardMedia component="img" sx={{ width: '100%', aspectRatio: 1 / 0.7 }} image={`data:image/jpeg;base64,${event.profileImgBody}`} alt="green iguana" />
                    <CardContent>
                        <Typography variant="h5" component="div">
                            {event.eventName}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Starts on: {new Date(event.startDate).toLocaleDateString('en-GB')} | Ends on: {new Date(event.endDate).toLocaleDateString('en-GB')}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Location: {event.city}, {event.state}, {event.country}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Prices starting from: {event.eventStartingPrice}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Created By: {event.createdBy}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Created On: {new Date(event.createdOn).toLocaleDateString('en-GB')}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Status: {event.acceptedBy ? 'Accepted' : event.rejectedBy ? 'Rejected' : 'Pending'}
                        </Typography>
                    </CardContent>
                </CardActionArea>
                <CardActions sx={{ display: 'flex', justifyContent: 'right' }}>
                    {((event.acceptedBy === null && event.rejectedBy === null) || (event.acceptedBy === null && event.createdBy === profile.administratorId)) && event.isActive && (
                        <>
                            <Button size="small" startIcon={<EditOutlined />} color="primary" variant="outlined">
                                Accept
                            </Button>
                            <Button size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                Reject
                            </Button>
                        </>
                    )}
                    {event.createdBy === profile.administratorId && event.isActive && event.isPublished === false && (
                        <>
                            <Button size="small" startIcon={<PublishOutlined />} color="primary" variant="outlined">
                                Publish
                            </Button>
                            <Button size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                Cancel
                            </Button>
                        </>
                    )}
                    {(event.isPublished && !event.isCancelled) && (
                        <>
                            {event.createdBy === profile.administratorId && (
                                <>
                                    <select value={event.registrationStatus} onChange={handleRegistrationStatusChange}>
                                        <option value={1}>Yet to Open</option>
                                        <option value={2}>Open</option>
                                        <option value={3}>Closed</option>
                                    </select>
                                    <Button size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                        Cancel
                                    </Button>
                                </>
                            )}
                            {event.createdBy !== profile.administratorId &&  (
                                <Button size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                    Cancel
                                </Button>
                            )}
                        </>
                    )}
                </CardActions>
        
            </Card>

            <ConfirmationDialog
                open={openDialog}
                title="Confirmation"
                content="Are you sure you want to proceed?"
                onConfirm={event.isPublished ? handlePublishConfirm : handleConfirm}
                onCancel={handleCancel}
            />

            {showAlert && <ReusableAlert message={alertMessage} severity={alertSeverity} duration={3000} onClose={() => setShowAlert(false)} />}
        </>
    );
};

export default OrganiserEventCard;
