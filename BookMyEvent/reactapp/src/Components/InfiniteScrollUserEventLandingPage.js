import React from "react";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import EventCard from "./EventCard";
import {
  IncrementHomePageNumber,
  fetchEvents,
} from "../Features/ReducerSlices/HomeEventsSlice";
import { setLoading } from "../Features/ReducerSlices/loadingSlice";
import store from "../App/store";

let cnt = 0;
const InfiniteScrollUserEventsLandingPage = () => {
  


    const dispatch = useDispatch();
  const events = useSelector((store) => store.homeEvents.events);
  


  const LoadEventOnPageEnd = async () => {
    const scrollPosition = document.documentElement.scrollTop;
    const contentHeight = document.documentElement.scrollHeight;
    const viewportHeight = window.innerHeight;
    if (
      scrollPosition + viewportHeight >= contentHeight - 100 &&
      !store.getState().homeEvents.loading && !store.getState().homeEvents.end
    ) {
      console.log(store.getState().homeEvents.page);
      await dispatch(
        fetchEvents({ pageNumber: store.getState().homeEvents.page })
      ).unwrap();
      dispatch(IncrementHomePageNumber());
    }
  };



  useEffect(() => {
    window.addEventListener("scroll", LoadEventOnPageEnd);
    return () => {
      window.addEventListener("scroll", LoadEventOnPageEnd);
    };
  }, []);




  return (
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
  );
};

export default InfiniteScrollUserEventsLandingPage;
