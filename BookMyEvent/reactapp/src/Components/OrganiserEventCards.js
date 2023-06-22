import * as React from 'react';
import { useEffect } from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { DeleteOutline, EditOutlined, PublishOutlined } from '@material-ui/icons'
import { Button, CardActionArea, CardActions } from '@mui/material';
import ConfirmationDialog from './ConfirmationDialog';
import { useDispatch, useSelector } from 'react-redux';
import { useState } from 'react';
import { deleteEvent } from '../Features/ReducerSlices/EventsSlice';
import ReusableAlert from './ReusableAlert';
import { Link ,useNavigate} from 'react-router-dom';
import store from '../App/store';
const OrganiserEventCard = ({ event }) => {
        const state = store.getState();
    const [openDialog, setOpenDialog] = useState(false);
    const [showAlert, setShowAlert] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");
    const [alertSeverity, setAlertSeverity] = useState("success");
    const dispatch = useDispatch();
    const profile = useSelector((state) => state.profile.info);
    const handleConfirm = async () => {
        setOpenDialog(false);
        var tempEvent = { ...event };
        tempEvent.updatedBy = profile.administratorId;
        console.log(tempEvent);
        await dispatch(deleteEvent(tempEvent)).unwrap();
        setAlertMessage(state.events.message);
        setAlertSeverity(state.events.error ? "error" : "success");
        setShowAlert(true);
    };

    const handleCancel = () => {
        setOpenDialog(false);
    };
    const handleOperation = () => {
        setShowAlert(true);
    };

    const handleCloseAlert = () => {
        setShowAlert(false);
    };
    const navigate = useNavigate();

    //useEffect(() => {
    //    setAlertMessage(state.events.message);
    //    setAlertSeverity(state.events.error ? "error" : "success");
    //    setShowAlert(true);
    //}, []);
    const handleClick = (id) => {
        navigate(`event/${id}`)
    }
    return (
        <>
            <Card sx={{ width: '400px', boxShadow: "0px 0px 9px #d0d0d0" }}>
                <CardActionArea onClick={() => handleClick(event.eventId)}>
                    <CardMedia
                        component="img"
                        sx={{ width: '100%', aspectRatio: 1 / 0.7 }}
                        image={`data:image/jpeg;base64,${event.profileImgBody}`}
                        alt="green iguana"
                    />
                    <CardContent>
                        <Typography variant="h5" component="div">
                            {event.eventName}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Starts on: {new Date(event.startDate).toLocaleDateString('en-GB')} | Ends on: {new Date(event.endDate).toLocaleDateString('en-GB')}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Location: {event.city},{event.state},{event.country}
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
                            Status: {event.acceptedBy ? "Accepted" : event.rejectedBy ? "Rejected" : "Pending"}
                        </Typography>

                    </CardContent>
                </CardActionArea>
                <CardActions sx={{ display: 'flex', justifyContent: 'right' }}>
                    <Button size="small" startIcon={<EditOutlined />} color="secondary" variant="outlined">
                        Edit
                    </Button>
                    <Button size="small" startIcon={<DeleteOutline />} color="error" variant="outlined" onClick={() => setOpenDialog(true)}>
                        Delete
                    </Button>
                    <Button size="small" startIcon={<PublishOutlined />} color="primary" variant="outlined">
                        Publish
                    </Button>
                </CardActions>
            </Card>

            <ConfirmationDialog
                open={openDialog}
                title="Confirmation"
                content="Are you sure you want to proceed?"
                onConfirm={handleConfirm}
                onCancel={handleCancel}
            />

            {showAlert && (
                <ReusableAlert
                    message={alertMessage}
                    severity={alertSeverity}
                    duration={3000}
                    onClose={handleCloseAlert}
                />
            )}
        </>
    );
}
export default OrganiserEventCard;
