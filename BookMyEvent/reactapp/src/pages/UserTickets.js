import React, { useEffect, useState } from 'react';
import { Typography } from '@material-ui/core';
import { useParams } from 'react-router-dom';
import TicketServices from '../Services/TicketServices'
import { useSelector } from 'react-redux';
import Ticket from '../Components/Ticket';
const UserTickets = () => {
    const { eventId } = useParams();
    const [tickets, setTickets] = useState([]);
    const auth = useSelector(store => store.auth);
    useEffect(() => {
        const loadTickets = async () => {
            const _tickets = await TicketServices().getAllUserEventTickets(auth.id, eventId);
            console.log(_tickets);
            setTickets(_tickets);
        }
        loadTickets();

    }, []);
    let cnt = 0;
    return (
        <div id="ticketComponent" style={{ padding: '30px',display:'flex',justifyContent:'center' }}>
            {tickets.length === 0 ? (
                <Typography variant="body1">No tickets available.</Typography>
            ) : (
                <div>
                    {tickets.map((ticket, index) => (<Ticket ticket={{ ticket: ticket, index: index, eventId: eventId }} />))}
                </div>
            )}
        </div>
    );
};

export default UserTickets;
