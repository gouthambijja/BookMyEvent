import { Call, LocationCity, Mail, MyLocation } from "@material-ui/icons";
import React, { useEffect } from "react";
import { fetchNoOfPastEvents, fetchOrganisationEvents } from '../Features/ReducerSlices/EventsSlice';
import { fetchEventsCreatedBy } from '../Features/ReducerSlices/EventsSlice';
import EmailIcon from '@material-ui/icons/Email';
import PhoneIcon from '@material-ui/icons/Phone';
import LocationOnIcon from '@material-ui/icons/LocationOn';
import { useDispatch, useSelector } from 'react-redux'
import { useState } from 'react';
import { fetchUserInfoByUserId } from "../Features/ReducerSlices/HomeEventsSlice";
import { makeStyles } from '@material-ui/core/styles';
import {
    Card, CardContent, CardHeader, Avatar, Typography, Container, Box,
    Grid, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField,
    Divider,
} from '@material-ui/core';
import Chip from '@mui/material/Chip';
import { updateOrganiserThunk, updateUserThunk } from "../Features/ReducerSlices/ProfileSlice";


const useStyles = makeStyles((theme) => ({

    card: {
        width: '100%',
        // height: '50%',
        margin: '0px',
        background: 'rgba(255,255,255,0.1)',
        color: "white",
        //border: '1px solid white',
        // backgroundImage:'url(https://images.pexels.com/photos/276205/pexels-photo-276205.jpeg?)',
        // boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)',

    },
    statastiCard: {
        width: '100%',
        // height: '40%',
        margin: '0',
        padding: '1%',
        boxShadow: '8px 4px 8px 4px rgba(0, 0, 0, 0.2)',
        borderRadius: theme.spacing(2),
        background: 'transparent'
    },
    editButton: {
        position: "absolute",
        // backgroundColor:theme.palette.secondary.light,
        right: "20px",
        top: "85px",
        zIndex: "9",
        // margin:'8px',

    },
    incompleteProfile: {
        position: "absolute",
        backgroundColor: theme.palette.warning.dark,
        right: "200px",
        padding: "6px 16px 6px 16px",
        top: "85px",
        zIndex: "9",
        // margin:'8px',

    },
    avatar: {

        width: "200px",
        height: "200px",
        margin: '10px auto auto auto',
        border: "6px dashed #fff"
    },
    header: {
        textAlign: 'center',


    },
    content: {
        textAlign: 'center',
    },
    icon: {
        marginRight: theme.spacing(1),
    },
    divider: {
        margin: theme.spacing(2, 0),
    },
}));
const ProfilePage = () => {
    const profile = useSelector((store) => store.profile.info);
    const auth = useSelector(store => store.auth);
    const registeredEventIds = useSelector(store => store.homeEvents.registeredEventIds);
    const NoOfTransactions = useSelector(store => store.homeEvents.NoOfTransactions);
    const amountSpent = useSelector(store => store.homeEvents.AmountSpent);
    let myEvents = useSelector((state) => state.events.myEvents);
    let orgEvents = useSelector((state) => state.events.organisationEvents);
    let NoOfOrganiserPastEvents = useSelector((state) => state.events.organiserNoOfPastEvents);
    let NoOfOrganisationPastEvents = useSelector((state) => state.events.organisationNoOfPastEvents);
    const [myPublishedEvents, setMyPublishedEvents] = useState([]);
    const [orgPublishedEvents, setOrgPublishedEvents] = useState([]);
    const [userData, setUserData] = useState(profile);

    const [open, setOpen] = useState(false);
    const dispatch = useDispatch();
    const handleEditProfile = async () => {
        setOpen(true);
    }
    const handleClose = () => {
        setUserData(profile)
        setOpen(false);
    };

    const handleSave = () => {

        console.log(userData);
        if (auth.role !== "User") {

            dispatch(updateOrganiserThunk(userData));
        }
        else if (auth.role === "User") {
            dispatch(updateUserThunk(userData));
        }

        setOpen(false);
    };
    const handleInputChange = (e) => {
        setUserData((prevData) => ({
            ...prevData,
            [e.target.name]: e.target.value,

        }));
    }
    useEffect(() => {
        const temp = async () => {
            if (NoOfOrganiserPastEvents == undefined || NoOfOrganiserPastEvents == 0 || NoOfOrganisationPastEvents == undefined || NoOfOrganisationPastEvents == 0) {

                var resp = await dispatch(fetchNoOfPastEvents({ organiserId: profile.administratorId, organisationId: profile.organisationId })).unwrap();
                console.log(resp);
            }
            if (orgEvents.length === 0) {
                await dispatch(fetchOrganisationEvents(profile.organisationId)).unwrap();
            }
            if (myEvents.length === 0) {
                await dispatch(fetchEventsCreatedBy(profile.administratorId)).unwrap();
            }
            if (myEvents.length !== 0 && orgEvents.length !== 0) {

                const mypublishedEvents = myEvents.filter((event) => event.isPublished == true);
                const orgpublishedEvents = orgEvents.filter((event) => event.isPublished == true);
                setMyPublishedEvents(mypublishedEvents);
                setOrgPublishedEvents(orgpublishedEvents);
            }
        }
        const fetchUserInfo = async () => {
            if (registeredEventIds.length == 0 || NoOfTransactions == 0 || amountSpent == 0) {
                var response = await dispatch(fetchUserInfoByUserId(profile.userId)).unwrap();
                console.log(response);
                console.log(registeredEventIds);

            }
        }
        if (auth.role !== "User" && auth.role !== "Admin") {
            console.log("hey Admin");
            temp();
        }
        else if (auth.role === "User") {
            fetchUserInfo();
        }
    }, []);
    const classes = useStyles();
    return (<div style={{ margin: '0px', display: "flex", borderBottom: '1px solid white', flexWrap: 'wrap', flexDirection: 'column', boxSizing: "border-box" }}>

        <Card className={classes.card} style={{ backgroundImage: 'url(https://img.freepik.com/premium-vector/modern-blue-pink-purple-dynamic-stripes-colorful-abstract-geometric-design-background-business-card-presentation-brochure-banner-wallpaper_249611-1029.jpg)', backgroundPosition: 'center top', backgroundSize: 'cover' }}>
            {(auth.role === "User" && (profile.userAddress === null || profile.phoneNumber === null)) ? <Chip className={classes.incompleteProfile} color="error" label="Incomplete profile!" /> : <></>}
            <Button onClick={handleEditProfile} variant="contained" className={classes.editButton} >{(auth.role === "User" && (profile.userAddress === null || profile.phoneNumber === null)) ? <>Update Profile </> : <>Edit Profile </>}</Button>
            <div style={{ position: 'absolute', top: '0', width: '100%', margin: 'auto', height: '30%' }}></div>
            {/* <div style={{ boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)', position: 'absolute', top: '0', width: '100%', margin: 'auto', height: '30%' }}></div> */}
            <Avatar
                className={classes.avatar}
                alt="Profile Image"
                src={profile.imgBody !== "" ? `data:image/jpeg;base64,${profile.imgBody}` : profile.imageName}


            />
            <div style={{ margin: '25px', width: '100%', display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', alignItems: 'center', border: '1px black solid', borderRadius: '10px', backdropFilter: "blur(60px) brightness(2)" }}>
                <div>
                    <CardHeader
                        className={classes.header}
                        title={
                            <Typography variant="subtitle2" component="h2" style={{ fontSize: '2rem', color: "black" }}>
                                {auth.role === 'User' ? profile.name : profile.administratorName}
                            </Typography>
                        }
                        subheader={
                            <Typography variant="caption" component="h3" style={{ fontSize: '1rem', color: 'grey' }}>
                                {auth.role}
                            </Typography>
                        }
                    ></CardHeader>


                </div>
                <div>
                    <Grid container alignItems="center">
                        <Grid item>
                            <EmailIcon className={classes.icon} style={{ color: 'blue' }} />
                        </Grid>
                        <Grid item>
                            <Typography variant="body1" style={{ color: 'black' }}>{profile.email}</Typography>
                        </Grid>
                    </Grid>
                </div>
                <div>
                    <Grid container alignItems="center">
                        <Grid item>
                            <PhoneIcon className={classes.icon} style={{ color: 'blue' }} />
                        </Grid>
                        <Grid item>
                            <Typography variant="body1" style={{ color: 'black' }}>{profile.phoneNumber}</Typography>
                        </Grid>
                    </Grid>
                </div>
                <div>
                    <Grid container alignItems="center">
                        <Grid item>
                            <LocationOnIcon className={classes.icon} style={{ color: 'red' }} />
                        </Grid>
                        <Grid item>
                            <Typography variant="body1" style={{ color: 'black' }}>{auth.role == "User" ? profile.userAddress : profile.administratorAddress}</Typography>
                        </Grid>
                    </Grid>
                </div>

            </div>
        </Card>
        {auth.role !== "Admin" ?
            <Card className={classes.statastiCard} >
                <div style={{ height: "100%", display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', alignItems: 'center', flexDirection: 'row' }}>

                    <Card style={{ height: "100%", maxWidth: '45%', boxShadow: '0 4px 8px 0 rgba(170, 205, 217, 1.5)', border: '1px solid white', color: 'white', display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', flexDirection: 'row', backgroundColor: "#cde1fa", backgroundImage: 'url(https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQUPPgVFN752lQvo_3_pQx14dix6z14QETOnt3fVimQ5RCUG5197DntCOMs4xAHWUqC9Pk&usqp=CAU)', backgroundPosition: 'center top', backgroundSize: 'cover' }}>
                        <div style={{ height: "100%", maxWidth: '100%', backdropFilter: "blur(6px) brightness(1)", display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', flexDirection: 'row', paddingBottom: "20px" }}>
                            <div style={{ textAlign: 'center', width: '100%' }}>

                                <CardHeader title={<Typography variant="h5" component="h2" style={{ fontSize: '2rem', width: '100%', color: 'black' }}>
                                    My Events
                                </Typography>} />
                            </div>
                            <Card style={{ margin: '1%', backgroundColor: '#dbc2ec', border: '1px black solid', borderRadius: '10px' }}>
                                <CardContent>
                                    <Typography variant="poster" component="h2" style={{ fontSize: '1.5rem', textAlign: 'center', fontWeight: "900" }}>
                                        {auth.role !== "User" ? myEvents.length : registeredEventIds.length}
                                    </Typography>
                                </CardContent>
                                <CardHeader title={auth.role !== "User" ? "Active Events" : "Registered Events"} titleTypographyProps={{ variant: 'h6' }} />
                            </Card>

                            <Card style={{ margin: '1%', backgroundColor: '#ccd0ff', border: '1px black solid', borderRadius: '10px' }}>
                                <CardContent> <Typography variant="poster" component="h2" style={{ fontSize: '1.5rem', textAlign: 'center', fontWeight: "900" }}>
                                    {auth.role !== 'User' ? myPublishedEvents.length : NoOfTransactions}
                                </Typography></CardContent>
                                <CardHeader title={auth.role !== "User" ? "Published Events" : "Transactions"} titleTypographyProps={{ variant: 'h6' }} />
                            </Card>

                            <Card style={{ margin: '1%', backgroundColor: '#72c2ff', border: '1px black solid', borderRadius: '10px' }}>
                                <CardContent> <Typography variant="poster" component="h2" style={{ fontSize: '1.5rem', textAlign: 'center', fontWeight: "900" }}>
                                    {auth.role !== 'User' ? NoOfOrganiserPastEvents : <><span>&#8377;{amountSpent}/-</span></>}
                                </Typography></CardContent>
                                <CardHeader title={auth.role !== 'User' ? "Past Events" : "Amount Spent"} titleTypographyProps={{ variant: 'h6' }} />
                            </Card>
                        </div>
                    </Card>
                    {auth.role !== 'User' ?
                        <Card style={{ height: "100%", maxWidth: '45%', boxShadow: '0 4px 8px 0 rgba(170, 205, 217, 1.5)', border: '1px solid white', color: 'white', display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', flexDirection: 'row', backgroundColor: "#cde1fa", backgroundImage: 'url(https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQUPPgVFN752lQvo_3_pQx14dix6z14QETOnt3fVimQ5RCUG5197DntCOMs4xAHWUqC9Pk&usqp=CAU)', backgroundPosition: 'center top', backgroundSize: 'cover' }}>
                            <div style={{ height: "100%", maxWidth: '100%', backdropFilter: "blur(6px) brightness(1)", display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', flexDirection: 'row', paddingBottom: "20px" }}>
                                <div style={{ textAlign: 'center', width: '100%' }}>
                                    <CardHeader title={<Typography variant="h5" component="h2" style={{ fontSize: '2rem', width: '100%', color: 'black' }}>
                                        Organisation Events
                                    </Typography>} />
                                </div>
                                <Card style={{ margin: '1%', backgroundColor: '#dbc2ec', border: '1px black solid', borderRadius: '10px' }}>
                                    <CardContent> <Typography variant="poster" component="h2" style={{ fontSize: '1.5rem', textAlign: 'center', fontWeight: "900" }}>
                                        {orgEvents.length}
                                    </Typography></CardContent>
                                    <CardHeader title="Active Events" titleTypographyProps={{ variant: 'h6' }} />
                                </Card>

                                <Card style={{ margin: '1%', backgroundColor: '#ccd0ff', border: '1px black solid', borderRadius: '10px' }}>
                                    <CardContent> <Typography variant="poster" component="h2" style={{ fontSize: '1.5rem', textAlign: 'center', fontWeight: "900" }}>
                                        {orgPublishedEvents.length}
                                    </Typography></CardContent>
                                    <CardHeader title="Published Events" titleTypographyProps={{ variant: 'h6' }} />
                                </Card>

                                <Card style={{ margin: '1%', backgroundColor: '#72c2ff', border: '1px black solid', borderRadius: '10px' }}>
                                    <CardContent> <Typography variant="poster" component="h2" style={{ fontSize: '1.5rem', textAlign: 'center', fontWeight: "900" }}>
                                        {NoOfOrganisationPastEvents}
                                    </Typography></CardContent>
                                    <CardHeader title="Past Events" titleTypographyProps={{ variant: 'h6' }} />
                                </Card>
                            </div>
                        </Card> : <></>}
                </div>
            </Card>
            : <></>}
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Edit Profile</DialogTitle>
            <DialogContent>
                <Container>
                    <Box>
                        <form>
                            <TextField
                                label="Name"
                                id={auth.role != "User" ? "administratorName" : "name"}
                                name={auth.role != "User" ? "administratorName" : "name"}
                                variant="outlined"
                                value={auth.role != "User" ? userData.administratorName : userData.name}
                                onChange={handleInputChange}
                                margin="normal"
                                required
                                fullWidth
                            />
                            <TextField
                                label="Address"
                                name={auth.role != "User" ? "administratorAddress" : "userAddress"}
                                id={auth.role != "User" ? "administratorAddress" : "userAddress"}
                                variant="outlined"
                                value={auth.role != "User" ? userData.administratorAddress : userData.userAddress}
                                onChange={handleInputChange}
                                margin="normal"
                                required
                                fullWidth
                            />
                            <TextField
                                label="Phone Number:"
                                id='phoneNumber'
                                name='phoneNumber'
                                variant="outlined"
                                type="number"
                                value={userData.phoneNumber}
                                onChange={handleInputChange}
                                margin="normal"
                                required
                                fullWidth
                            />
                        </form>
                    </Box>
                </Container>

            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose} color="secondary">
                    Cancel
                </Button>
                <Button onClick={handleSave} color="primary">
                    Save
                </Button>
            </DialogActions>
        </Dialog>
    </div>

    );
};

export default ProfilePage;
