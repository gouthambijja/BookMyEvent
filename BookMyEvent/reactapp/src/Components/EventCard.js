import * as React from "react";
import { useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import { Button, CardActionArea, CardActions } from "@mui/material";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const EventCard = ({ event }) => {
  const categories = useSelector((store) => store.category);
  const navigate = useNavigate();
  const handleEvent = (eventId) => {
    navigate(`event/${eventId}`);
    console.log("inside");
  };

  const handleEventRegister = () => {
    navigate(`/registerEvent/${event.eventId}/${event.formId}`);
  };
  return (
    <Card
      className="card"
      sx={{ maxWidth: "400px", boxShadow: "0px 0px 9px #d0d0d0" }}
    >
      <CardActionArea onClick={() => handleEvent(event.eventId)}>
        <div style={{ position: "relative",width:'100%' }}>
          <div>
            <CardMedia
              component="img"
              sx={{ height:'280px', aspectRatio: 1 / 0.7 }}
              image={`data:image/jpeg;base64,${event.profileImgBody}`}
              alt="green iguana"
            />
            <div
              style={{ position: "absolute", transform: "translate(0%,-105%)",paddingLeft:'20px',color:'#000',width:'90%',borderTopRightRadius:'30px',borderBottomRightRadius:'30px',background:'rgb(255,255,255,0.4)',backdropFilter:'blur(6px)' }}
            >
              <Typography variant="h4" component="div">
                <div
                  style={{
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                    fontFamily: "serif",
                  }}
                >
                  {event.eventName}
                </div>
              </Typography>
              <Typography variant="h6" component="div">
                <div
                  style={{
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                  }}
                >
                  {
                    categories?.categories?.find(e => e.categoryId == Number(event?.categoryId)).categoryName
                  }
                </div>
              </Typography>
            </div>
          </div>

          <CardContent>
            <div
              style={{
                whiteSpace: "nowrap",
                overflow: "hidden",
                textOverflow: "ellipsis",
              }}
            >
              <Typography variant="subtitle1" color="text.secondary">
                {event.startDate?.split("T")[0]} - {event.endDate.split("T")[0]}
              </Typography>
              <Typography variant="subtitle1" color="text.secondary">
                {event.location},
              </Typography>
              <Typography variant="subtitle1" color="text.secondary">
                {event.city},{event.state}, {event.country}
              </Typography>
              <Typography variant="body1" color="text.primary">
                {event.eventStartingPrice == 0 ? (
                  <span style={{ color: "green" }}>Free</span>
                ) : (
                  `Ticket price starts from ₹${event.eventStartingPrice}/- `
                )}
              </Typography>
              <Typography variant="subtitle1" color="text.secondary">
                {event.registrationStatusId == 1?"Registration starts soon....":`Available Seats :${(event.availableSeats == -1)?event.capacity:event.availableSeats}`}
              </Typography>

            </div>
          </CardContent>
        </div>
      </CardActionArea>
      <div style={{ display: "flex", justifyContent: "end" }}>
        <Button
          size="small"
          color="primary"
          sx={{ padding: "10px" }}
          onClick={handleEventRegister}
          disabled = {event.registrationStatusId==1 || event.availableSeats == 0?true:false}
        >
          Register 
        </Button>
      </div>
    </Card>
  );
};
export default EventCard;
