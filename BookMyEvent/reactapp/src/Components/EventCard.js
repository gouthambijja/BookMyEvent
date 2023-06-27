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
      sx={{ width: "400px", boxShadow: "0px 0px 9px #d0d0d0" }}
    >
      <CardActionArea onClick={() => handleEvent(event.eventId)}>
        <div style={{ position: "relative" }}>
          <div>
            <CardMedia
              component="img"
              sx={{ width: "100%", aspectRatio: 1 / 0.7 }}
              image={`data:image/jpeg;base64,${event.profileImgBody}`}
              alt="green iguana"
            />
          </div>
          <div
            style={{
              position: "absolute",
              top: "45%",
              left:"2%",
              width: "70%",
             padding:'0px 20px',
             borderRadius:'30px',
              background:"rgba(255,255,255,0.4)",
              backdropFilter:"blur(6px)"
            }}
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
                {categories.categories[Number(event.categoryId)]?.categoryName}
              </div>
            </Typography>
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
           
                {event.eventStartingPrice == 0
                  ? "Free"
                  : `Tickets starts from ${event.eventStartingPrice}`}
            </Typography>
            </div>
          </CardContent>
        </div>
      </CardActionArea>
      <div style={{display:'flex',justifyContent:'end'}}>
        <Button size="small" color="primary" sx={{padding:'10px'}}onClick={handleEventRegister}>
          Register
        </Button>
      </div>
    </Card>
  );
};
export default EventCard;
