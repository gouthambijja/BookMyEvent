import React, { useEffect, useState } from "react";
import QRCodeGenerator from "./QRCodeGenerator";
const Ticket = ({ ticket }) => {
  const tkt = ticket.ticket;
  console.log(tkt);
  const index = ticket.index;
  const event = ticket.event;

  let cnt = 0;
  return (
    <div style={{textAlign:'center',minWidth:'260px',border:'1px solid #3f50b5',borderRadius:'4px',padding:'30px'}}>
      <section >
        <div className="row">
          <article className="card fl-left" style={{ position: "relative" }}>
            <section className="date">
                <span>{index + 1}</span>
            </section>
            <section className="card-cont">
              <h3>{event.eventName}</h3>
              <div className="even-date">
                <i className="fa fa-calendar"></i>
                <time>
                  <div>
                    from:{new Date(`${event?.startDate}`).toDateString()}
                  </div>
                  <div style={{ marginTop: "2px" }}>
                    Ends on:{new Date(`${event?.endDate}`).toDateString()}
                  </div>
                </time>
              </div>
              <p>
                {event?.city}, {event?.state}, {event?.country}
              </p>
              <div>
                {tkt.userDetails.map((user) => (
                  <div key={cnt++} style={{ fontFamily: "Calibri" }}>
                    {user.Label}:
                    <i>
                      {user.StringResponse
                        ? user.StringResponse
                        : user.NumberResponse
                        ? user.NumberResponse
                        : user.DateResponse}
                    </i>
                  </div>
                ))}
              </div>
              <div style={{padding:'10px'}}>
                <QRCodeGenerator data={JSON.stringify(tkt?.ticket)} />
              </div>
            </section>
          </article>
        </div>
      </section>
    </div>
  );
};
export default Ticket;
