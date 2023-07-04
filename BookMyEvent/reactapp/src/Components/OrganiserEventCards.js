import * as React from 'react';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { acceptEvent, deleteEvent, rejectEvent, updateEvent } from '../Features/ReducerSlices/EventsSlice';
import { Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Typography } from '@mui/material';
import { DeleteOutline, EditOutlined, PublishOutlined } from '@material-ui/icons';
import ConfirmationDialog from './ConfirmationDialog';
import ReusableAlert from './ReusableAlert';
import { Link, useNavigate } from 'react-router-dom';
import store from '../App/store';
import { toast } from 'react-toastify';

const OrganiserEventCard = ({ event }) => {
    const state = store.getState();
    const [openDialog, setOpenDialog] = useState(false);
    const [openDialogtoAccept, setOpenDialogtoAccept] = useState(false);
    const [showAlert, setShowAlert] = useState(false);
    const [publish, setPublish] = useState(false);
    const [accept, setAccept] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');
    const [alertSeverity, setAlertSeverity] = useState('success');
    const dispatch = useDispatch();
    const profile = useSelector((state) => state.profile.info);
    const auth= useSelector((state) => state.auth);
    const navigate = useNavigate();
    let myEvents = useSelector((state) => state.events.myEvents);

    const handleConfirm = async () => {
        setOpenDialog(false);
        const tempEvent = { ...event };
        tempEvent.updatedBy = profile.administratorId;

        await dispatch(deleteEvent(tempEvent)).unwrap();
        toast.error("Event Deleted!")
        // setAlertMessage(state.events.message);
        // setAlertSeverity(state.events.error ? 'error' : 'success');
        // setShowAlert(true);
        setOpenDialog(false);

    };

    const handleDeleteOperation = () => {
        setPublish(false);
        setOpenDialog(true);
    };

    const handleCancel = () => {
        setOpenDialogtoAccept(false);
        setOpenDialog(false);

    };

    const handlePublish = () => {
        setPublish(true);
        setOpenDialog(true);
    };

    const handlePublishConfirm = () => {
        setOpenDialog(false);
        const tempEvent = { ...event };
        tempEvent.isPublished = true;
        dispatch(updateEvent(tempEvent));
        toast.success("Event Published!")
        setOpenDialog(false);

    };
    const handleAcceptEvent=()=>{
       setAccept(true);
       setOpenDialogtoAccept(true);
       

    }
    const handleAcceptConfirm=()=>{
        const tempEvent = { ...event };
        tempEvent.acceptedBy=profile.administratorId;
        tempEvent.updatedBy=profile.administratorId;
        console.log("accept event");
        dispatch(acceptEvent(tempEvent));
      setOpenDialogtoAccept(false);

        toast.success("Event Accepted!")


    }
    const handleRejectEvent=()=>{
        setAccept(false);
      setOpenDialogtoAccept(true);

    }
    const handleRejectConfirm=()=>{
        const tempEvent = { ...event };
        tempEvent.rejectedBy=profile.administratorId;
        tempEvent.updatedBy=profile.administratorId;
        console.log(event);
        console.log(tempEvent);
        dispatch(rejectEvent(tempEvent));
      setOpenDialogtoAccept(false);
        toast.error("Event Rejected!")

    }
    const handleRegistrationStatusChange = (e) => {
        const tempEvent = { ...event };
        tempEvent.registrationStatusId = parseInt(e.target.value);
        dispatch(updateEvent(tempEvent));
        toast.success("Status changed!")
    };

    const handleClick = () => {
        navigate(`/organiser/event/${event.eventId}`);
    };
    const handleEdit=()=>{
        navigate(`/organiser/EditEvent/${event.eventId}`);
    }
   React.useEffect(()=>{
    console.log("events changed")
   },[myEvents])
    return (
        <>
            <Card sx={{ width: '400px', boxShadow: '0px 0px 9px #d0d0d0', display: 'flex', flexDirection: "column", height: "auto" }}>
                <CardActionArea onClick={handleClick} sx={{ flexBasis: "90%" }}>
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
                <CardActions sx={{ display: 'flex', justifyContent: 'right', flexBasis: "10%" }}>
                    {((event.acceptedBy === null && event.rejectedBy === null) && (auth.role=="Owner" || auth.role=="Secondary_Owner")) && event.isActive && (
                        <>
                            <Button onClick={()=>handleAcceptEvent()} size="small" startIcon={<EditOutlined />} color="primary" variant="outlined">
                                Accept
                            </Button>
                            <Button onClick={()=>handleRejectEvent()} size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                Reject
                            </Button>
                        </>
                    )}
                    {(event.createdBy === profile.administratorId || auth.role=="Owner" || auth.role=="Secondary_Owner") && (event.acceptedBy !== null) && event.isActive && event.isPublished === false && (
                        <>
                             <Button onClick={()=>handleEdit()} size="small" startIcon={<EditOutlined />} color="primary" variant="outlined">
                                Edit
                            </Button>
                            <Button onClick={()=>handlePublish(event)} size="small" startIcon={<PublishOutlined />} color="primary" variant="outlined">
                                Publish
                            </Button>
                            <Button onClick={()=> handleDeleteOperation()} size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                Cancel 
                            </Button>
                        </>
                    )}
                    {(event.isPublished && !event.isCancelled) && (
                        <>
                            {(event.createdBy === profile.administratorId || auth.role=="Owner" || auth.role=="Secondary_Owner") && (
                                <>
                                    <select value={event.registrationStatusId} style={{padding:'6px',marginRight:'10px'}} onChange={handleRegistrationStatusChange}>
                                        <option value={1}>Yet to Open</option>
                                        <option value={2}>Open</option>
                                        <option value={3}>Closed</option>
                                    </select>
                                    <Button onClick={()=> handleDeleteOperation()}  size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                        Cancel
                                    </Button>
                                </>
                            )}
                            {/* {event.createdBy !== profile.administratorId &&  (
                                <Button size="small" startIcon={<DeleteOutline />} color="error" variant="outlined">
                                    Cancel 3
                                </Button>
                            )} */}
                        </>
                    )}
                </CardActions>
        
            </Card>

            <ConfirmationDialog
                open={openDialog}
                title="Confirmation"
                content={publish?"Are you sure you want to publish?":"Are you sure to delete?"}
                onConfirm={publish ? handlePublishConfirm : handleConfirm}
                onCancel={handleCancel}
            />
            <ConfirmationDialog
                open={openDialogtoAccept}
                title="Confirmation"
                content={accept?"Are you sure you want to accept this Event?":"Are you sure to reject this Event?"}
                onConfirm={accept ? handleAcceptConfirm : handleRejectConfirm}
                onCancel={handleCancel}
            />

  
        </>
    );
};

export default OrganiserEventCard;
