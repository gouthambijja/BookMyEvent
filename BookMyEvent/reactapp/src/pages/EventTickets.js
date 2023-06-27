import React, { useEffect, useState } from 'react';
import { Typography, Card, CardContent } from '@material-ui/core';
import { useParams } from 'react-router-dom';
import TicketServices from '../Services/TicketServices'
import { useSelector } from 'react-redux';
import {  Box } from "@mui/material";


const EventTickets= () => {
    const [tickets,setTickets] = useState([]);
    const auth = useSelector(store => store.auth);
    const {eventId} = useParams();
    console.log(eventId);
  useEffect(()=>{
   const loadTickets = async() =>{
    console.log(eventId);
        const _tickets = await TicketServices().getEventTickets(eventId);
        setTickets(_tickets);
    }
    loadTickets();
    
  },[]);
let cnt  = 0 ;
  return (
    <div style={{padding:'30px'}}>
      {tickets.length === 0 ? (
        <Typography variant="body1">No tickets available.</Typography>
      ) : (
        <div style={{maxWidth:'800px',margin:'0 auto'}}>
          {tickets.map((ticket,index) => (
            <Card key={cnt++} variant="outlined" style={{marginBottom:'10px'}}>
              <CardContent>
                <Typography variant="h5" component="h2">
                  {index + 1}
                </Typography>
                <Box >
                  {ticket.userDetails.map(user =>
                    <div key = {cnt++}>
                        {user.Label}:<i>{user.StringResponse?user.StringResponse:user.NumberResponse?user.NumberResponse:user.DateResponse}</i>
                    </div>
                    )}
                </Box>
              </CardContent>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
};

export default EventTickets;
