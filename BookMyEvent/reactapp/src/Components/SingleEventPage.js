import { useSelector } from "react-redux";
import React, { useEffect, useRef, useState } from "react";
import Typography from "@mui/material/Typography";
import { useParams } from "react-router-dom";
import eventsServices from "../Services/EventServices";
import { Paper, Button, Box } from "@mui/material";
import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { makeStyles } from "@material-ui/core/styles";
import { CenterFocusStrong } from "@material-ui/icons";
import CategoryIcon from "@material-ui/icons/Category";
import { useNavigate } from "react-router-dom";
const useStyles = makeStyles((theme) => ({
    carouselContainer: {
        width: "100%",
        height: 500,
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
    useEffect(() => {
        const temp = async () => {
            const images = await eventsServices().getEventImages(id);
            if (event === undefined) {
                const event = await eventsServices().getEventById(id);
                setEvent(event);
            }
            setImages(images);
            setAutoPlay(true);
        };
        temp();
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
        transitionTime: 500, // Set the transition duration (in milliseconds)
    };
    const navigate = useNavigate();
    const handleRegister = () => {
        navigate(`/registerEvent/${event.eventId}/${event.formId}`);
    };
    return (
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
            <Box className={classes.eventBox} sx={{ marginTop: "-84px" }}>
                <div
                    className={"singleEventPageBg"}
                    style={{ display: "flex", flexWrap: "wrap", gap: "10px", paddingBottom: '20px' }}
                >
                    <Box sx={{ flexBasis: "100%", color: "#fff", zIndex: "30", background: 'rgba(0,0,0,0.2)' }}>
                        <Typography variant="h2" component="div">
                            {event?.eventName}
                        </Typography>
                    </Box>
                    <Typography
                        variant="h6"
                        component="div"
                        className={classes.description}
                    >
                        {event?.description}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        <span className={classes.heading}> Category:</span>
                        {categories?.categories?.find(e => e.categoryId == Number(event?.categoryId))?.categoryName}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        <span className={classes.heading}>Location:</span>
                        {event?.location}, {event?.city},{event?.state}, {event?.country}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        <span className={classes.heading}>Price Range :</span>
                        {event?.eventStartingPrice} to {event?.eventEndingPrice}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        <span className={classes.heading}>Start Date :</span>
                        {event?.startDate?.split("T")[0]}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        {" "}
                        <span className={classes.heading}>End Date :</span>
                        {event?.endDate?.split("T")[0]}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        {" "}
                        <span className={classes.heading}>Capacity :</span>
                        {event?.capacity}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        {" "}
                        <span className={classes.heading}>Seats Available:</span>
                        {event?.availableSeats == -1 ? event?.capacity : event?.availableSeats}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        {event?.eventStartingPrice == 0
                            ? "Free"
                            : `Tickets starts from ${event?.eventStartingPrice}`}
                    </Typography>
                    <Typography
                        variant="h6"
                        component="span"
                        className={`${classes.category} ${classes.hoverEffect}`}
                    >
                        {(event?.registrationStatusId == 1) ? <span>registrations opening soon...</span> : <></>}
                    </Typography>

                    <Button
                        onClick={handleRegister}
                        sx={{
                            border: "2px solid #757ce8",
                            flexBasis: "100%",
                            borderRadius: "30px",
                            "&:hover": { letterSpacing: "3px" },
                        }}
                        disabled={event?.registrationStatusId == 1 || event?.availableSeats == 0 ? true : false}
                    >
                        Register
                    </Button>
                </div>
            </Box>
        </Box>
    );
};
export default SingleEventPage;
