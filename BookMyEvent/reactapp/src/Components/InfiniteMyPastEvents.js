import React, { useState } from "react";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import store from "../App/store";
import { fetchOrganiserPastEvents } from "../Features/ReducerSlices/EventsSlice";
import OrganiserEventCard from "./OrganiserEventCards";

let cnt = 0;
const InfiniteMyPastEvents = () => {

    var tempPageNo = useSelector((store) => store.events.myPastEventsPageNo);
    const [pageNumber, setPageNumber] = useState(2);
    const [pageSize, setPageSize] = useState(10);
    const dispatch = useDispatch();
    var profInfo = useSelector((store) => store.profile.info);
    var paginationDetails = {
        organiserId : profInfo.administratorId,
        pageSize :  pageSize 
    }
    //const isFetchedAlready = useSelector((store) => store.events.isMyPastEventsFetched);
    //if (isFetchedAlready) {
    //    dispatch(fetchOrganiserPastEvents(profInfo.administratorId));
    //}
    let events = useSelector((store) => store.events.myPastEvents);
    const LoadEventOnPageEnd = async () => {
        const scrollPosition = document.documentElement.scrollTop;
        const contentHeight = document.documentElement.scrollHeight;
        const viewportHeight = window.innerHeight;
        if (scrollPosition + viewportHeight >= contentHeight - 100 && !store.getState().events.loading && !store.getState().events.myPastEventsEnd) {
            await dispatch(fetchOrganiserPastEvents({...paginationDetails,pageNumber:store.getState().events.myPastEventsPageNo})).unwrap();
            console.log(store.getState().events.myPastEvents)
            console.log(events)
        }
    };

    useEffect(() => {
      
        LoadEventOnPageEnd();
        console.log("I am in my past events");
      
    }, []);
    useEffect(() => {
        window.addEventListener("scroll", LoadEventOnPageEnd);
        return () => {
          window.addEventListener("scroll", LoadEventOnPageEnd);
        };
      });

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
            {events.length}
            {events.length > 0 ?
            
             events.map((event) => (
                <OrganiserEventCard event={event} />
            ))
            
            : <h1>No events to show here</h1>}
        
        </div>
    );
};

export default InfiniteMyPastEvents;
