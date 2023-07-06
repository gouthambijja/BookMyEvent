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
    border: '1px solid white',
    // backgroundImage:'url(https://images.pexels.com/photos/276205/pexels-photo-276205.jpeg?)',
    // boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)',

  },
  statastiCard: {
    width: '100%',
    // height: '40%',
    marginTop: '3%',
    padding: '1%',
    boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)',
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
    backgroundColor:theme.palette.warning.dark,
    right: "200px",
    padding:"6px 16px 6px 16px",
    top: "85px",
    zIndex: "9",
    // margin:'8px',

  },
  avatar: {

    width: "300px",
    height: "300px",
    marginLeft: '6%',
    marginTop: "5vh",
    marginBottom: theme.spacing(2),
    border: '10px solid white',
    // borderBottomRightRadius:'none',
    borderBottom: 'none',
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
    if(auth.role !== "User"){

      dispatch(updateOrganiserThunk(userData));
    }
    else if(auth.role==="User"){
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
    else if(auth.role === "User"){
      fetchUserInfo();
    }
  }, []);
  const classes = useStyles();
  return (<div style={{ margin: '0px', display: "flex", borderBottom: '1px solid white', flexWrap: 'wrap', flexDirection: 'column', backgroundImage: 'url(https://images.pexels.com/photos/276223/pexels-photo-276223.jpeg)', backgroundPosition: 'center', backgroundSize: 'cover' }}>
    <Card className={classes.card}>
      {(auth.role==="User" && (profile.userAddress===null || profile.phoneNumber===null))?<Chip  className={classes.incompleteProfile}  color="error" label="Incomplete profile!" />  :<></>}
      <Button onClick={handleEditProfile} variant="contained" className={classes.editButton} >{(auth.role==="User" && (profile.userAddress===null || profile.phoneNumber===null))? <>Update Profile </>:<>Edit Profile </>}</Button>
      <div style={{ boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)', position: 'absolute', top: '0', width: '100%', margin: 'auto', height: '30%', backgroundImage: 'url(https://images.pexels.com/photos/276223/pexels-photo-276223.jpeg)', backgroundPosition: 'center', backgroundSize: 'cover' }}></div>
      {/* <div style={{ boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)', position: 'absolute', top: '0', width: '100%', margin: 'auto', height: '30%', backgroundImage: 'url(https://kiranworkspace.com/demo/projects/code-snippets/card/profile-card/img/banner.jpg)', backgroundPosition: 'center', backgroundSize: 'cover' }}></div> */}
      <Avatar
        className={classes.avatar}
        alt="Profile Image"
        src={profile.imgBody !== "" ? `data:image/jpeg;base64,${profile.imgBody}` : profile.imageName}
      />
      <div style={{ margin: '0px', width: '100%', display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', alignItems: 'center' }}>
        <div>
          <CardHeader
            className={classes.header}
            title={
              <Typography variant="h5" component="h2" style={{ fontSize: '2rem' }}>
                {auth.role === 'User' ? profile.name : profile.administratorName}
              </Typography>
            }
            subheader={
              <Typography variant="subtitle1" component="h3" style={{ fontSize: '1.2rem' }}>
                {auth.role}
              </Typography>
            }
          ></CardHeader>


        </div>
        <div>
          <Grid container alignItems="center">
            <Grid item>
              <EmailIcon className={classes.icon} />
            </Grid>
            <Grid item>
              <Typography variant="body1">{profile.email}</Typography>
            </Grid>
          </Grid>
        </div>
        <div>
          <Grid container alignItems="center">
            <Grid item>
              <PhoneIcon className={classes.icon} />
            </Grid>
            <Grid item>
              <Typography variant="body1">{profile.phoneNumber}</Typography>
            </Grid>
          </Grid>
        </div>
        <div>
          <Grid container alignItems="center">
            <Grid item>
              <LocationOnIcon className={classes.icon} />
            </Grid>
            <Grid item>
              <Typography variant="body1">{auth.role == "User" ? profile.userAddress : profile.administratorAddress}</Typography>
            </Grid>
          </Grid>
        </div>

      </div>
    </Card>
  {auth.role !== "Admin"?
    <Card className={classes.statastiCard}>
      <div style={{ height: "100%", display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', alignItems: 'center', flexDirection: 'row' }}>

        <Card style={{ height: "100%", maxWidth: '45%', background: 'rgba(255,255,255,0.1)', border: '2px solid white', color: 'white', display: "flex", flexWrap: 'wrap', padding: '1%', justifyContent: 'space-around', flexDirection: 'row' }}>
          <div style={{ textAlign: 'center', width: '100%' }}>

            <CardHeader title={<Typography variant="h5" component="h2" style={{ fontSize: '2rem', width: '100%' }}>
              My Events
            </Typography>} />
          </div>
          <Card style={{ margin: '1%' }}>
            <CardContent>
              <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
                {auth.role !== "User" ? myEvents.length : registeredEventIds.length}
              </Typography>
            </CardContent>
            <CardHeader title={auth.role !== "User" ? "Active Events" : "Registered Events"} />
          </Card>

          <Card style={{ margin: '1%' }}>
            <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
              {auth.role !== 'User' ? myPublishedEvents.length : NoOfTransactions}
            </Typography></CardContent>
            <CardHeader title={auth.role !== "User" ? "Published Events" : "Transactions"} />
          </Card>

          <Card style={{ margin: '1%' }}>
            <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
              {auth.role !== 'User' ? NoOfOrganiserPastEvents : <><span>&#8377;{amountSpent}/-</span></>}
            </Typography></CardContent>
            <CardHeader title={auth.role !== 'User' ? "Past Events" : "Amount Spent"} />
          </Card>
        </Card>
        {auth.role !== 'User' ?
          <Card style={{ height: "100%", maxWidth: '45%', background: 'rgba(255,255,255,0.1)', border: '2px solid white', color: 'white', padding: '1%', display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', flexDirection: 'row' }}>
            <div style={{ textAlign: 'center', width: '100%' }}>
              <CardHeader title={<Typography variant="h5" component="h2" style={{ fontSize: '2rem', width: '100%' }}>
                Organisation Events
              </Typography>} />
            </div>
            <Card style={{ margin: '1%' }}>
              <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
                {orgEvents.length}
              </Typography></CardContent>
              <CardHeader title="Active Events" />
            </Card>

            <Card style={{ margin: '1%' }}>
              <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
                {orgPublishedEvents.length}
              </Typography></CardContent>
              <CardHeader title="Published Events" />
            </Card>

            <Card style={{ margin: '1%' }}>
              <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
                {NoOfOrganisationPastEvents}
              </Typography></CardContent>
              <CardHeader title="Past Events" />
            </Card>
          </Card> : <></>}
      </div>
    </Card>
    :<></>}
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Edit Profile</DialogTitle>
      <DialogContent>
        <Container>
          <Box>
            <form>
              <TextField
                label="Name"
                id={auth.role != "User"?"administratorName":"name"}
                name={auth.role != "User"?"administratorName":"name"}
                variant="outlined"
                value={auth.role != "User"?userData.administratorName:userData.name}
                onChange={handleInputChange}
                margin="normal"
                required
                fullWidth
              />
              <TextField
                label="Address"
                name={auth.role != "User"?"administratorAddress":"userAddress"}
                id={auth.role != "User"?"administratorAddress":"userAddress"}
                variant="outlined"
                value={auth.role != "User"?userData.administratorAddress:userData.userAddress}
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
