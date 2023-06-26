import React, { useEffect, useState } from 'react';
import { Typography, Card, CardContent } from '@material-ui/core';
import { useParams } from 'react-router-dom';
import TicketServices from '../Services/TicketServices'
import { useSelector } from 'react-redux';

const UserTickets= () => {
  const {eventId} = useParams();
  const [tickets,setTickets] = useState([]);
  const auth = useSelector(store => store.auth);
  useEffect(()=>{
   const loadTickets = async() =>{
        const _tickets = await TicketServices().getAllUserEventTickets(auth.id,eventId);
        console.log(_tickets);
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
                <Typography variant="body2" component="p">
                  {ticket.userDetails.map(user =>
                    <div key = {cnt++}>
                        {user.Label}:<i>{user.StringResponse?user.StringResponse:user.NumberResponse?user.NumberResponse:user.DateResponse}</i>
                    </div>
                    )}
                </Typography>
              </CardContent>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
};

export default UserTickets;
