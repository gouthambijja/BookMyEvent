import { useSelector } from 'react-redux';
import React, { useEffect, useRef, useState } from 'react';
import Typography from '@mui/material/Typography';
import { useParams } from 'react-router-dom';
import eventsServices from "../Services/EventServices";
import { Paper, Button, Box } from '@mui/material';
import { Carousel } from 'react-responsive-carousel';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import { makeStyles } from '@material-ui/core/styles';
import { CenterFocusStrong } from '@material-ui/icons';
const useStyles = makeStyles((theme) => ({
  carouselContainer: {
    width: '100%',
    height: 500,
    position: 'relative',
    margin: "0",
    padding: "0",
  },
  carouselSlide: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
    height: '100%',
  },
  carouselImage: {
    width: '100%',
    height: '100%',
    objectFit: 'cover',
  },
  carouselDots: {
    position: 'absolute',
    bottom: 10,
    left: '50%',
    transform: 'translateX(-50%)',
    display: 'flex',
    justifyContent: 'center',
  },
  carouselDot: {
    width: 10,
    height: 10,
    borderRadius: '50%',
    backgroundColor: '#ccc',
    margin: '0 5px',
    cursor: 'pointer',
  },
  activeDot: {
    backgroundColor: '#333',
  },
  eventBox:{
    backgroundColor:"white",

  },
  eventName: {
    fontSize: '30px',
    fontWeight: 'bold',
    marginBottom: theme.spacing(2),
    textShadow: '2px 2px 4px rgba(0, 0, 0, 0.3)',
    textAlign: 'center',
    color: theme.palette.primary.main
  },
  description: {
    marginBottom: theme.spacing(2),
    textAlign: 'center',
    fontSize: '20px',

  },
  heading: {
    fontWeight: 'bold',
  },
  detailsdiv: {
    marginTop: theme.spacing(8),
  },
  category: {
    margin: theme.spacing(20),
    backgroundColor: '#f5f5f5',
    padding: theme.spacing(2),
    borderRadius: '4px',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.5)',
  },
  subtitle: {
    marginBottom: theme.spacing(1),
    color: '#888888',
  },
  location: {
    margin: theme.spacing(20),
    backgroundColor: '#f5f5f5',
    padding: theme.spacing(2),
    borderRadius: '4px',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
  },
  priceRange: {
    marginBottom: theme.spacing(1),
  },
}));
const SingleEventPage = () => {
  const { id } = useParams();
  const [images, setImages] = useState([]);
  const events = useSelector((store) => store.homeEvents.events);
  const categories = useSelector(store => store.category);
  const event = events.find((e => e.eventId == id));
  const classes = useStyles();
  console.log(event);
  useEffect(() => {
    const temp = async () => {

      const images = await eventsServices.getEventImages(id);
      setImages(images);
      console.log(images);
    }
    temp();

  }, [])
  const carouselSettings = {
    showThumbs: false,
    autoPlay: true,
    interval: 3000,
    infiniteLoop: true,
    stopOnHover: false,
    showStatus: true,
    showIndicators: true,
    showArrows: true,
    swipeable: true,
    transitionTime: 500,// Set the transition duration (in milliseconds)

  };

  return (
    <Box>

      <Carousel {...carouselSettings}>
        {images.map((image, index) => (
          <Paper key={index} elevation={0} className={classes.carouselContainer}>
            <div className={classes.carouselSlide}>
              <img src={`data:image/jpeg;base64,${image.imgBody}`} alt={`Image ${index + 1}`} className={classes.carouselImage} />
            </div>
          </Paper>
        ))}
      </Carousel >

      <Box p={2} className={classes.eventBox}>
        <Typography variant="h4" component="div" className={classes.eventName}>Event Name:
          {event.eventName}
        </Typography>
        <Typography variant="h6" component="div" className={classes.description}>
          {event.description}
        </Typography>
        <div className={classes.detailsdiv}>
          <Typography variant="h6" component="span" className={classes.category}><span className={classes.heading}>Category:</span>
            {categories.categories[Number(event.categoryId)]?.categoryName}
          </Typography>
          <Typography variant="h6" component="span" className={classes.category}><span className={classes.heading}>Location:</span>
            {event.location}, {event.city},{event.state}, {event.country}
          </Typography>
          <Typography variant="h6" component="span" className={classes.category}><span className={classes.heading}>Price Range :</span>
            {event.eventStartingPrice} to {event.eventEndingPrice}
          </Typography>
        </div>
        <div className={classes.detailsdiv}>
          <Typography variant="h6" component="span" className={classes.category}><span className={classes.heading}>Start Date :</span>
            {event.startDate?.split("T")[0]}
          </Typography>
          <Typography variant="h6" component="span" className={classes.category}> <span className={classes.heading}>End Date :</span>
            {event.endDate.split("T")[0]}
          </Typography>
          <Typography variant="h6" component="span" className={classes.category}> <span className={classes.heading}>Capacity :</span>
            {event.capacity}
          </Typography>
          <Typography variant="h6" component="span" className={classes.category}> <span className={classes.heading}>Seats Available:</span>
            {event.availableSeats}
          </Typography>
        </div>

        {/* <Typography variant="body1" color="text.primary">
        {event.eventStartingPrice == 0 ? "Free" : `Tickets starts from ${event.eventStartingPrice}`}
      </Typography> */}
      </Box>
    </Box>

  );
}
export default SingleEventPage;