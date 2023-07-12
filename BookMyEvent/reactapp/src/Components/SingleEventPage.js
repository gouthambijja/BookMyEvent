import { useSelector } from "react-redux";
import React, { useEffect, useState } from "react";
import Typography from "@mui/material/Typography";
import { useParams, useNavigate } from "react-router-dom";
import eventsServices from "../Services/EventServices";
import { Paper, Button, Box, Grid, Card, CardContent, Avatar, Divider } from "@mui/material";
import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { makeStyles } from "@material-ui/core/styles";
import CategoryIcon from "@material-ui/icons/Category";
import LocationOnIcon from "@material-ui/icons/LocationOn";
import EventIcon from "@material-ui/icons/Event";
import PeopleIcon from "@material-ui/icons/People";
import AttachMoneyIcon from "@material-ui/icons/AttachMoney";
import CalendarTodayIcon from "@material-ui/icons/CalendarToday";

const useStyles = makeStyles((theme) => ({

    carouselContainer: {
        width: "100%",
        height: '600px',
        position: "relative",
        margin: "0",
        padding: "0",
    },
    carouselSlide: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        width: "100%",
        height: "100%",
    },
    carouselImage: {
        width: "100%",
        height: "100%",
        objectFit: "cover",
    },
    carouselDots: {
        position: "absolute",
        bottom: 10,
        left: "50%",
        transform: "translateX(-50%)",
        display: "flex",
        justifyContent: "center",
    },
    carouselDot: {
        width: 10,
        height: 10,
        borderRadius: "50%",
        backgroundColor: "#ccc",
        margin: "0 5px",
        cursor: "pointer",
    },
    activeDot: {
        backgroundColor: "#333",
    },
    eventBox: {},
    description: {
        textAlign: "center",
        fontSize: "20px",
        flexBasis: "100%",
        fontStyle: "italic",
        padding: theme.spacing(2),
        borderRadius: "4px",
    },
    heading: {
        fontWeight: "bold",
        width: "300px",
        maxWidth: "90%",
        textAlign: "left",
    },
    category: {
        marginBottom: "10px",
        flexBasis: "80%",
        padding: theme.spacing(2),
    },
    hoverEffect: {
        transition: "background-color 0.3s ease",
        "&:hover": {
            scale: "1.01",
        },
    },
    subtitle: {
        marginBottom: theme.spacing(1),
        color: "#888888",
    },
    location: {
        margin: theme.spacing(20),
        backgroundColor: "#f5f5f5",
        padding: theme.spacing(2),
        borderRadius: "4px",
        boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
    },
    priceRange: {
        marginBottom: theme.spacing(1),
    },
}));

const SingleEventPage = () => {
    const { id } = useParams();
    const [images, setImages] = useState([]);
    const categories = useSelector((store) => store.category);
    const events = useSelector((store) => store.homeEvents.events);
    const [event, setEvent] = useState(events.find((e) => e.eventId == id));
    const classes = useStyles();
    const [autoPlay, setAutoPlay] = useState(false);
    const navigate = useNavigate();
    var curDate = new Date().toLocaleDateString('en-US');
    useEffect(() => {
        const fetchData = async () => {
            const images = await eventsServices().getEventImages(id);
            if (event === undefined) {
                const event = await eventsServices().getEventById(id);
                setEvent(event);
            }
            setImages(images);
            setAutoPlay(true);
        };
        fetchData();
    }, []);

    const carouselSettings = {
        showThumbs: false,
        autoPlay: autoPlay,
        interval: 3000,
        infiniteLoop: true,
        stopOnHover: false,
        showStatus: true,
        showIndicators: true,
        showArrows: true,
        swipeable: true,
        transitionTime: 500
    };

    const handleRegister = () => {
        navigate(`/registerEvent/${event.eventId}/${event.formId}`);
    };

    return (
        <div>
            <Box>
                <Carousel {...carouselSettings}>
                    {images.map((image, index) => (
                        <Paper
                            key={index}
                            elevation={0}
                            className={classes.carouselContainer}
                        >
                            <div className={classes.carouselSlide}>
                                <img
                                    src={`data:image/jpeg;base64,${image.imgBody}`}
                                    alt={`Image ${index + 1}`}
                                    className={classes.carouselImage}
                                />
                            </div>
                        </Paper>
                    ))}
                </Carousel>

                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <Box py={2} style={{ backdropFilter: "blur(60px) brightness(2)", color: "black", backgroundColor: "#ffb103 ", padding: '20px', fontWeight: "900", position:'relative' }}>
                            <Typography variant="h2" style={{ fontWeight: "900" }}>{event?.eventName}</Typography>
                            <div style={{ backgroundColor: "white", position: 'absolute', right: '20px', transform: 'translateY(-100%)', textAlign: 'center', border: '5px solid black', minWidth: '200px', display: 'flex', flexDirection: "column", padding:'20px' }}>
                                <div style={{ flexBasis:'10%' }}>
                                    Event starts in
                                </div>
                                <div style={{ color: "#3f50b5", fontWeight: "400px", fontSize: '60px', flexBasis: '80%' }} >
                                    {Math.floor((new Date(event?.startDate) - new Date()) / (1000 * 60 * 60 * 24))}                                </div>
                                <div style={{ flexBasis: '10%' }}>
                                days
                                </div>
                            </div>
                        </Box>
                    </Grid>
                    <div style={{ display: 'flex', justifyContent: 'space-around', height: 'auto', width: '100%', padding: '10px 32px', gap: '20px' }} >
                        <Grid item xs={12} sm={6} >
                            <Card style={{ width: "100%", height: '100%' }}>
                                <CardContent style={{ display: 'flex', justifyContent: 'around' ,height:'100%'}}>
                                    <Avatar
                                        src={event?.profileImgBody !== "" ? `data:image/jpeg;base64,${event?.profileImgBody}` : "Event"}
                                        alt="Event Profile Pic"
                                        sx={{height: '200px', weight: '200px', marginBottom: '2', flexBasis: "50%" }}
                                        variant="rounded"
                                    />
                                    <Typography variant="body1" className="customScrollbar"
                                        sx={{ flexBasis: "50%", padding: "20px", boxSizing: "border-box",overflowY:'scroll' ,height:'200px'}}
                                    >{event?.description}</Typography>
                                </CardContent>
                            </Card>
                        </Grid>

                        <Grid item xs={12} sm={6} >
                            <Card style={{ height: '100%' }}>
                                <CardContent>
                                    <Grid container spacing={2} alignItems="center">
                                        <Grid item xs={12} sm={6}>
                                            <Typography variant="subtitle1" style={{ color: "#af59ea", display: 'flex', alignItems: 'center', gap: '3px' }}>
                                                <CategoryIcon /> Category
                                            </Typography>
                                            <Typography variant="body1">
                                                {categories?.categories?.find(
                                                    (e) => e.categoryId == Number(event?.categoryId)
                                                )?.categoryName}
                                            </Typography>
                                        </Grid>

                                        <Grid item xs={12} sm={6}>
                                            <Typography variant="subtitle1" style={{ color: "#af59ea", display: 'flex', alignItems: 'center', gap: '3px' }}>
                                                <LocationOnIcon /> Location
                                            </Typography>
                                            <Typography variant="body1">
                                                {event?.location}, {event?.city}, {event?.state},{" "}
                                                {event?.country}
                                            </Typography>
                                        </Grid>

                                        <Grid item xs={12} sm={6}>
                                            <Typography variant="subtitle1" style={{ color: "#af59ea", display: 'flex', alignItems: 'center', gap: '3px' }}>
                                                <EventIcon /> Date
                                            </Typography>
                                            <Typography variant="body1">{new Date(event?.startDate).toLocaleDateString('en-GB')} - {new Date(event?.endDate).toLocaleDateString('en-GB')}</Typography>
                                        </Grid>

                                        <Grid item xs={12} sm={6}>
                                            <Typography variant="subtitle1" style={{ color: "#af59ea", display: 'flex', alignItems: 'center', gap: '3px' }}>
                                                <PeopleIcon /> Capacity
                                            </Typography>
                                            <Typography variant="body1">{event?.capacity}</Typography>
                                        </Grid>

                                        <Grid item xs={12} sm={6}>
                                            <Typography variant="subtitle1" style={{ color: "#af59ea", display: 'flex', alignItems: 'center', gap: '3px' }}>
                                                <AttachMoneyIcon /> Price Range
                                            </Typography>
                                            <Typography variant="body1">
                                                {event?.eventStartingPrice} - {event?.eventEndingPrice}
                                            </Typography>
                                        </Grid>

                                        <Grid item xs={12} sm={6}>
                                            <Typography variant="subtitle1" style={{ color: "#af59ea", display: 'flex', alignItems: 'center', gap: '3px' }}>
                                                <CalendarTodayIcon /> Available Seats
                                            </Typography>
                                            <Typography variant="body1">
                                                {event?.availableSeats == -1
                                                    ? event?.capacity
                                                    : event?.availableSeats}
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                </CardContent>
                            </Card>
                        </Grid>
                    </div>

                    <Grid item xs={12}>
                        <Paper className={classes.registerButton} elevation={0} style={{ display: 'flex', justifyContent: 'center', marginBottom:'10px' }}>
                            <Button
                                onClick={handleRegister}
                                variant="contained"
                                sx={{ backgroundColor:"#3f50b5" }}
                                disabled={
                                    event?.registrationStatusId == 1 || event?.availableSeats == 0
                                }
                            >
                                Register
                            </Button>
                        </Paper>
                    </Grid>
                </Grid>
            </Box>
        </div>
    );
};

export default SingleEventPage;
