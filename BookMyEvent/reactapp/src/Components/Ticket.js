import React, { useEffect, useState } from "react";
import "../pages/Ticket.css";
import EventServices from '../Services/EventServices'
const Ticket = ({ ticket }) => {
    const tkt = ticket.ticket;
    console.log(tkt);
    const index = ticket.index;
    const eventId = ticket.eventId;
    const [event, setEvent] = useState({});
    useEffect(() => {
        const loadevent = async () => {
            const Event = await EventServices().getEventById(eventId);
            console.log(Event);
            setEvent(Event);
        }
        loadevent();
    }, []);
    let cnt = 0;
    return (
        <div >
            <section class="container" >
                <div class="row">
                    <article class="card fl-left" style={{position:'relative'}}>
                        <section class="date">
                            <time datetime="23th feb">
                                <span>{index + 1}</span>
                            </time>
                        </section>
                        <section class="card-cont">
                            <h3>{event.eventName}</h3>
                            <div class="even-date">
                                <i class="fa fa-calendar"></i>
                                <time>
                                    <span>from:{new Date(`${event.startDate}`).toDateString()}</span>
                                    <span style={{ marginTop: '2px' }}>Ends on:{new Date(`${event.endDate}`).toDateString()}</span>
                                </time>
                            </div>
                            <p>
                                {event.city}, {event.state}, {event.country}
                            </p>
                            <div >
                                {tkt.userDetails.map(user =>
                                    <div key={cnt++} style={{fontFamily:'Calibri'}}>
                                        {user.Label}:<i>{user.StringResponse ? user.StringResponse : user.NumberResponse ? user.NumberResponse : user.DateResponse}</i>
                                    </div>
                                )}</div>
                        </section>
                    </article>
                </div>
            </section>
        </div>
    );
}
export default Ticket;