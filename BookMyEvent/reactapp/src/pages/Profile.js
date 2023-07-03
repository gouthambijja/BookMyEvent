import { Call, LocationCity, Mail, MyLocation } from "@material-ui/icons";
import React, { useEffect } from "react";
import { fetchOrganisationEvents,fetchOrganisationNoOfPastEvents,fetchOrganiserNoOfPastEvents } from '../Features/ReducerSlices/EventsSlice';
import { fetchEventsCreatedBy } from '../Features/ReducerSlices/EventsSlice';
import EmailIcon from '@material-ui/icons/Email';
import PhoneIcon from '@material-ui/icons/Phone';
import LocationOnIcon from '@material-ui/icons/LocationOn';
import { useDispatch, useSelector } from 'react-redux'
import { useState } from 'react';
import { fetchAmountByUserId, fetchNoOfRegisteredTransactions, fetchUserRegisteredEventIds} from "../Features/ReducerSlices/HomeEventsSlice";
import { makeStyles } from '@material-ui/core/styles';
import {
  Card,
  CardContent,
  CardHeader,
  Avatar,
  Typography,
  Grid,
  Divider,
} from '@material-ui/core';


const useStyles = makeStyles((theme) => ({

  card: {
    width: '100%',
    // height: '50%',
    margin: '0px',
    boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)',
    borderRadius: theme.spacing(2),
  },
  statastiCard: {
    width: '100%',
    // height: '40%',
    marginTop: '3%',
    padding:'1%',
    boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)',
    borderRadius: theme.spacing(2),
  },
  avatar: {

    width: "300px",
    height: "300px",

    marginLeft: '6%',
    marginTop: "5vh",
    marginBottom: theme.spacing(2),
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
  let NoOfOrganiserPastEvents=useSelector((state) => state.events.organiserNoOfPastEvents);
  let NoOfOrganisationPastEvents=useSelector((state) => state.events.organisationNoOfPastEvents);
  const [myPublishedEvents,setMyPublishedEvents]=useState([]);
  const [orgPublishedEvents,setOrgPublishedEvents]=useState([]);
  const dispatch=useDispatch();
  
  useEffect(() => {
    const temp=async()=>{
     
    if (orgEvents.length === 0) {
      await dispatch(fetchOrganisationEvents(profile.organisationId)).unwrap();
    }
    if (myEvents.length === 0) {
     await dispatch(fetchEventsCreatedBy(profile.administratorId)).unwrap();
      }
      if(myEvents.length !== 0 && orgEvents.length !==0){

        const mypublishedEvents = myEvents.filter((event) => event.isPublished==true);
        const orgpublishedEvents = orgEvents.filter((event) => event.isPublished==true);
        setMyPublishedEvents(mypublishedEvents);
       setOrgPublishedEvents(orgpublishedEvents);
      }
      if(NoOfOrganiserPastEvents ==undefined || NoOfOrganiserPastEvents==0){
        await dispatch(fetchOrganiserNoOfPastEvents(profile.administratorId)).unwrap();
      }
      if(NoOfOrganisationPastEvents ==undefined || NoOfOrganisationPastEvents==0){
        await dispatch(fetchOrganisationNoOfPastEvents(profile.organisationId)).unwrap();
      }
    }
    const fetchUserInfo = async()=>{
      if(registeredEventIds.length ==0){
        await dispatch(fetchUserRegisteredEventIds(profile.userId)).unwrap();
      }
      if(NoOfTransactions == 0){
        await dispatch(fetchNoOfRegisteredTransactions(profile.userId)).unwrap();
      }
      if(amountSpent == 0){
        await dispatch(fetchAmountByUserId(profile.userId)).unwrap();
      }
    }
    if(auth.role !=="User"){
    temp();
    }
    else{
      fetchUserInfo();
    }
}, [myEvents,orgEvents]);
  const classes = useStyles();
  return (<div style={{  margin: '0px', display: "flex", flexWrap: 'wrap', flexDirection: 'column' }}>
    <Card className={classes.card}>
      <div style={{ boxShadow: '0 4px 8px 0 rgba(0, 0, 0, 0.2)', position: 'absolute', top: '0', width: '100%', margin: 'auto', height: '30%', backgroundImage: 'url(https://kiranworkspace.com/demo/projects/code-snippets/card/profile-card/img/banner.jpg)', backgroundPosition: 'center', backgroundSize: 'cover' }}></div>
      <Avatar
        className={classes.avatar}
        alt="Profile Image"
        src={profile.imgBody !== "" ? `data:image/jpeg;base64,${profile.imgBody}` : profile.imageName}
      />
      <div style={{ margin: '0px',width:'100%', display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', alignItems: 'center' }}>
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
       <Grid  container alignItems="center">
          <Grid item>
            <EmailIcon className={classes.icon} />
          </Grid>
          <Grid item>
            <Typography variant="body1">{profile.email}</Typography>
          </Grid>
        </Grid>
       </div>
       <div>
       <Grid  container alignItems="center">
          <Grid item>
            <PhoneIcon className={classes.icon} />
          </Grid>
          <Grid item>
            <Typography variant="body1">{profile.phoneNumber}</Typography>
          </Grid>
        </Grid>
       </div>
       <div>
       <Grid  container alignItems="center">
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

    <Card className={classes.statastiCard}>
      <div style={{ height: "100%", display: "flex", flexWrap: 'wrap', justifyContent: 'space-around', alignItems: 'center', flexDirection: 'row' }}>

        <Card style={{ height: "100%", maxWidth:'40%', display: "flex", flexWrap: 'wrap',padding:'1%', justifyContent: 'space-around',  flexDirection: 'row' }}>
       <div style={{ textAlign:'center', width: '100%' }}>

        <CardHeader title={<Typography variant="h5" component="h2" style={{ fontSize: '2rem', width: '100%' }}>
          My Events
        </Typography>} />
       </div>
          <Card style={{margin:'1%'}}>
            <CardContent>
              <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
               {auth.role !== "User"?myEvents.length:registeredEventIds.length} 
              </Typography>
            </CardContent>
            <CardHeader title={auth.role !== "User"?"Active Events":"Registered Events"} />
          </Card>

          <Card style={{margin:'1%'}}>
            <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
              {auth.role !=='User'?myPublishedEvents.length:NoOfTransactions}
            </Typography></CardContent>
            <CardHeader title={auth.role !== "User"?"Published Events":"Transactions"} />
          </Card>

          <Card style={{margin:'1%'}}>
            <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
              {auth.role !=='User'?NoOfOrganiserPastEvents:amountSpent}
            </Typography></CardContent>
            <CardHeader title={auth.role !=='User'?"Past Events":"Amount Spent"} />
          </Card>
        </Card>
          {auth.role !=='User'?
        <Card style={{ height: "100%",maxWidth:'40%', padding:'1%',  display: "flex", flexWrap: 'wrap', justifyContent: 'space-around',  flexDirection: 'row' }}>
       <div style={{ textAlign:'center', width: '100%' }}>
       <CardHeader title={<Typography variant="h5" component="h2" style={{ fontSize: '2rem', width: '100%' }}>
          Organisation Events
        </Typography>} />
          </div>
          <Card style={{margin:'1%'}}>
            <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
              {orgEvents.length}
            </Typography></CardContent>
            <CardHeader title="Active Events" />
          </Card>

          <Card style={{margin:'1%'}}>
            <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
              {orgPublishedEvents.length}
            </Typography></CardContent>
            <CardHeader title="Published Events" />
          </Card>

          <Card style={{margin:'1%'}}>
            <CardContent> <Typography variant="h5" component="h2" style={{ fontSize: '2rem', textAlign: 'center' }}>
              {NoOfOrganisationPastEvents}
            </Typography></CardContent>
            <CardHeader title="Past Events" />
          </Card>
        </Card> : <></> }
      </div>
    </Card>
  </div>

  );
};

export default ProfilePage;
