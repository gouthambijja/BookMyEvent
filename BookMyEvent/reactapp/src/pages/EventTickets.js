import React, { useEffect, useState } from "react";
import { Typography, Card, CardContent } from "@material-ui/core";
import { useParams } from "react-router-dom";
import TicketServices from "../Services/TicketServices";
import { useSelector } from "react-redux";
import { Box } from "@mui/material";
import Ticket from "../Components/Ticket";
import EventServices from "../Services/EventServices";

const EventTickets = () => {
    const { eventId } = useParams();
    const [tickets, setTickets] = useState([]);
    useEffect(() => {
        const loadTickets = async () => {
          
            const _tickets = await TicketServices().getEventTickets(eventId);
            //console.log(_tickets);
            setTickets(_tickets);
        }
        loadTickets();

    }, []);
    const [event, setEvent] = useState({});
    useEffect(() => {
      const loadevent = async () => {
        const Event = await EventServices().getEventById(eventId);
        //console.log(Event);
        setEvent(Event);
      };
      loadevent();
    }, []);
    let cnt = 0;
  return (
    <div
      id="ticketComponent"
      style={{
        padding: "30px",
        display: "flex",
        justifyContent: "center",
        flexWrap: "wrap",
        gap: "10px",
      }}
    >
      {tickets.length === 0 ? (
        <Typography variant="body1">No tickets available.</Typography>
      ) : (
        <>
          {tickets.map((ticket, index) => (
            <Ticket ticket={{ ticket: ticket, index: index, event: event }} />
          ))}
        </>
      )}
    </div>
  );
};

export default EventTickets;
