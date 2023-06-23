import React, { useState } from 'react';
import { Typography, Card, CardContent } from '@material-ui/core';

const UserTicketList = () => {
  const [tickets, setTickets] = useState([
    { id: 1, title: 'Ticket 1', description: 'Description for Ticket 1' },
    { id: 2, title: 'Ticket 2', description: 'Description for Ticket 2' },
    { id: 3, title: 'Ticket 3', description: 'Description for Ticket 3' },
  ]);

  return (
    <div>
      <Typography variant="h4">User Tickets</Typography>
      {tickets.length === 0 ? (
        <Typography variant="body1">No tickets available.</Typography>
      ) : (
        <div>
          {tickets.map((ticket) => (
            <Card key={ticket.id} variant="outlined">
              <CardContent>
                <Typography variant="h5" component="h2">
                  {ticket.title}
                </Typography>
                <Typography variant="body2" component="p">
                  {ticket.description}
                </Typography>
              </CardContent>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
};

export default UserTicketList;
