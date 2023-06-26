import React, { useEffect, useState } from "react";
import TransationServices from "../Services/TransationServices";
import { useSelector } from "react-redux";
import {
  Button,
  Card,
  CardContent,
  CardMedia,
  Typography,
} from "@mui/material";
import { useNavigate } from "react-router-dom";

const RegisteredEvents = () => {
  const navigate = useNavigate();
  const auth = useSelector((store) => store.auth);
  const [events, setEvents] = useState([]);
  let cnt = 0;
  useEffect(() => {
    const loadRegisteredEvents = async () => {
      const registeredEvents =
        await TransationServices.getAllRegisteredEventsByUserid(auth.id);
      setEvents(registeredEvents);
    };
    loadRegisteredEvents();
    return ()=>{
      loadRegisteredEvents();
  }
  }, []);
  const handleViewTickets = async(eventId) =>{
    navigate(`/tickets/${eventId}`)
  }
  return (
    <div style={{width:'100%',padding:'30px'}}>
      {events.map((event,index) => (
        <div key={cnt++} style={{margin:'0px auto',maxWidth:'800px',marginBottom:'10px'}}>
          <Card >
            <CardMedia
              component="img"
              height="200"
              image={`data:image/jpeg;base64,${event.profileImgBody}`}
              alt={event.eventName}
              sx={{objectFit:'cover',objectPosition:'center'}}
            />
            <CardContent sx={{display:'flex' ,justifyContent:'space-between'}}>
              <Typography variant="h5" component="div">
                {event.eventName}
              </Typography>
              <Button variant="contained" color="primary" onClick={()=>handleViewTickets(event.eventId)}>
                View Tickets
              </Button>
            </CardContent>
          </Card>
        </div>
      ))}
    </div>
  );
};

export default RegisteredEvents;
