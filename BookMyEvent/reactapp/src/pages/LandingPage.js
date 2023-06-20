import Button from '@mui/material/Button';
import React from 'react';
import { makeStyles } from "@material-ui/core/styles";
import { useNavigate } from "react-router-dom";
import EventCard from '../Components/EventCard';




const useStyles = makeStyles({
  container: {
    display: 'flex',
    justifyContent: 'flex-end',
  },
  button: {
    margin: '5rem',
    padding: '0.75rem 1.5rem',
    borderRadius: '4px',
    fontWeight: 'bold',
    fontSize: '1rem',
    backgroundColor: '#2196f3',
    color: '#ffffff',
    '&:hover': {
      backgroundColor: '#1976d2',
    },
  },
});

const LandingPage = () => {
  const navigate = useNavigate();

  const classes = useStyles();
  const handleOrganiserLogin=()=>{
    navigate("organiser/login");
  }
  const handleAdminLogin=()=>{
    navigate("admin/login");
  }
  return (<>
    <div className={classes.container} style={{display:'flex',justifyContent:"center",flexWrap:'wrap',gap:'20px',padding:'40px'}}>
    <EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/><EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/><EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/><EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/><EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/>
    <EventCard/>
      {/* <Button variant="contained" className={classes.button} onClick={handleOrganiserLogin}>
        Login as an Organiser
      </Button>
      <Button variant="contained" className={classes.button} onClick={handleAdminLogin}>
        Login as an Admin
      </Button> */}
    </div>
    </>
     
  )
}

export default LandingPage
