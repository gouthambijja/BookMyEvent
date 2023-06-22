import CircularProgress from '@mui/material/CircularProgress';
import Button from "@mui/material/Button";
import React from "react";
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
import InfiniteScrollUserEventsLandingPage from "../Components/InfiniteScrollUserEventLandingPage";

const useStyles = makeStyles({
  container: {
    display: 'flex',
    justifyContent: 'flex-end',
  },
  button: {
    margin: '5rem',
    padding: '0.75rem 1.5rem',
    borderRadius: '4px',
    fontWeight: 'bold',
    fontSize: '1rem',
    backgroundColor: '#2196f3',
    color: '#ffffff',
    '&:hover': {
      backgroundColor: '#1976d2',
    },
  },
});

const LandingPage = () => {
  const navigate = useNavigate();

  const classes = useStyles();
  const dispatch = useDispatch();

  const HomeEventPageNumber = useSelector((store) => store.homeEvents.page);
  const end = useSelector(store => store.homeEvents.end)
  const handleFilter = async (filters) => {
    filters = {
      ...filters,
      pageNumber: 1,
    };
    dispatch(clearHomeEvents());
    dispatch(SetPageNumber(2));
    await dispatch(fetchEvents(filters)).unwrap();
  };

  return (
    <>
      <div className={classes.container}>
        <EventsFilter onFilter={handleFilter} />
        <InfiniteScrollUserEventsLandingPage />
        {!end?<div style={{padding:'40px',textAlign:'center',fontStyle:'italic'}}><span>Loading...</span></div>
        :<div style={{fontStyle:'italic',padding:'40px',textAlign:'center'}}>New Events Coming soon!!</div>}
      </div>
    </>
  );
};

export default LandingPage;
