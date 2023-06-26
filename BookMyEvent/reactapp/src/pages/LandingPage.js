import CircularProgress from "@mui/material/CircularProgress";
import Button from "@mui/material/Button";
import React, { useState } from "react";
import { useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import { useNavigate } from "react-router-dom";
import EventCard from "../Components/EventCard";
import { useDispatch, useSelector } from "react-redux";
import {
  IncrementHomePageNumber,
  SetPageNumber,
  clearHomeEvents,
  fetchEvents,
  setLoading,
} from "../Features/ReducerSlices/HomeEventsSlice";
import EventsFilter from "../Components/EventsFilter";
import store from "../App/store";

const useStyles = makeStyles({
  button: {
    margin: "5rem",
    padding: "0.75rem 1.5rem",
    borderRadius: "4px",
    fontWeight: "bold",
    fontSize: "1rem",
    backgroundColor: "#2196f3",
    color: "#ffffff",
    "&:hover": {
      backgroundColor: "#1976d2",
    },
  },
});

const LandingPage = () => {
  let cnt = 0;
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const events = useSelector((store) => store.homeEvents.events);  
  const classes = useStyles();
  const HomeEventPageNumber = useSelector((store) => store.homeEvents.page);
  const end = useSelector((store) => store.homeEvents.end);
  const [_filters, setfilters] = useState({
    pageNumber: 1,
  });
  useEffect(()=>{
    LoadEventOnPageEnd();
  },[_filters])
  const handleFilter = async (filters) => {
    dispatch(clearHomeEvents());
    dispatch(SetPageNumber(1));
    setfilters(filters);
    console.log("HEY");
    // await dispatch(fetchEvents(filters)).unwrap();
  };
  const LoadEventOnPageEnd = async () => {
    const scrollPosition = document.documentElement.scrollTop;
    const contentHeight = document.documentElement.scrollHeight;
    const viewportHeight = window.innerHeight;
    if (
      scrollPosition + viewportHeight >= contentHeight - 100 &&
      !store.getState().homeEvents.loading &&
      !store.getState().homeEvents.end
    ) {
      console.log(
        _filters
      );
      await dispatch(
        fetchEvents({
          ..._filters,
          pageNumber: store.getState().homeEvents.page,
        })
      ).unwrap();
      dispatch(IncrementHomePageNumber());
    }
  };

  useEffect(() => {
    LoadEventOnPageEnd();
  }, [_filters]);
  useEffect(() => {
    window.addEventListener("scroll", LoadEventOnPageEnd);
    return () => {
      window.addEventListener("scroll", LoadEventOnPageEnd);
    };
  });
  const scrollPosition = document.documentElement.scrollTop;
  return (
    <>
      <div className={classes.container}>
        <EventsFilter onFilter={handleFilter} />
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            flexWrap: "wrap",
            gap: "10px",
            marginTop: "60px",
          }}
        >
          {events.map((event) => (
            <div key={cnt++}>
              <EventCard event={event} />
            </div>
          ))}
        </div>
        {!end && scrollPosition ? (
          <div
            style={{
              padding: "40px",
              textAlign: "center",
              fontStyle: "italic",
            }}
          >
            <span>Loading...</span>
          </div>
        ) : (
          <div
            style={{
              fontStyle: "italic",
              padding: "40px",
              textAlign: "center",
            }}
          >
            New Events Coming soon!!
          </div>
        )}
      </div>
    </>
  );
};

export default LandingPage;
