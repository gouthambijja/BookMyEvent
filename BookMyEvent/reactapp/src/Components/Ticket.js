import React, { useEffect, useState } from "react";
import QRCodeGenerator from "./QRCodeGenerator";
import { Box, Button, Modal, Typography, makeStyles } from "@material-ui/core";
import { useSelector } from "react-redux";
const styles = makeStyles({
  ticket: {
    textAlign: "center",
    minWidth: "260px",
    //   border: "1px solid #3f50b5",
    boxShadow: "0px 0px 2px grey",
    borderRadius: "4px",
    padding: "30px",
    transition: "box-shadow 0.1s linear",
    "&:hover": {
      boxShadow: "0px 0px 5px grey",
      border: "none",
      cursor: "pointer",
    },
  },
});
const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "90vw",
  height: "90vh",
  background: "linear-gradient(-45deg, #ee7752, #e73c7e, #23a6d5, #23d5ab)",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};
const Ticket = ({ ticket }) => {
  const filetypes = useSelector((store) => store.formFields.fileTypes);
  console.log(filetypes);
  const classes = styles();
  const tkt = ticket.ticket;
  // console.log(tkt);
  const index = ticket.index;
  const event = ticket.event;
  let cnt = 0;
  const [open, setOpen] = React.useState({});
  const handleOpen = (index) =>
    setOpen((prev) => {
      return { ...prev, [index]: true };
    });
  const handleClose = (index) =>
    setOpen((prev) => {
      return { ...prev, [index]: false };
    });
  return (
    <div className={classes.ticket}>
      <section>
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
                    <div>
                      {user.Label}:
                      {user.FileResponse == "" ? (
                        <i>
                          {user.StringResponse
                            ? user.StringResponse
                            : user.NumberResponse
                            ? user.NumberResponse
                            : user.DateResponse}
                        </i>
                      ) : (
                        <div>
                          <Button
                            onClick={() =>
                              handleOpen(user?.UserInputFormFieldid)
                            }
                          >
                            View{" "}
                            {
                              filetypes?.find(
                                (e) => e.fileTypeId == user.FileTypeId
                              )?.fileTypeName
                            }
                          </Button>
                          <Modal
                            open={open[user?.UserInputFormFieldid]}
                            onClose={() =>
                              handleClose(user?.UserInputFormFieldid)
                            }
                            aria-labelledby="modal-modal-title"
                            aria-describedby="modal-modal-description"
                            className="customScollbar"
                          >
                            <div style={style}>
                              <embed
                                className={"customScrollbar"}
                                src={`data:${
                                  filetypes?.find(
                                    (e) => e.fileTypeId == user.FileTypeId
                                  )?.fileTypeName
                                };base64,${user.FileResponse} `}
                                width="100%"
                                height="100%"
                              />
                            </div>
                          </Modal>
                        </div>
                      )}
                    </div>
                  </div>
                ))}
              </div>
              <div style={{ padding: "10px" }}>
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
