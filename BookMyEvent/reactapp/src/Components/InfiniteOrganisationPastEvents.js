import React, { useState } from "react";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import store from "../App/store";
import { fetchOrganisationPastEvents } from "../Features/ReducerSlices/EventsSlice";
import OrganiserEventCard from "./OrganiserEventCards";

let cnt = 0;
const InfiniteOrganisationPastEvents = () => {

    var tempPageNo = useSelector((store) => store.events.organisationPastEventsPageNo);
    // const [pageNumber, setPageNumber] = useState(tempPageNo);
    const [pageSize, setPageSize] = useState(10);
    const dispatch = useDispatch();
    var profInfo = useSelector((store) => store.profile.info);
    var paginationDetails = {
        organisationId: profInfo.organisationId,
        
        pageSize :  pageSize 
    }
  
    let events = useSelector((store) => store.events.organisationPastEvents);
    const LoadEventOnPageEnd = async () => {
        const scrollPosition = document.documentElement.scrollTop;
        const contentHeight = document.documentElement.scrollHeight;
        const viewportHeight = window.innerHeight;
        if (scrollPosition + viewportHeight >= contentHeight - 100 && !store.getState().events.loading && !store.getState().events.orgPastEventsEnd) {
            await dispatch(fetchOrganisationPastEvents({...paginationDetails,pageNumber:store.getState().events.organisationPastEventsPageNo})).unwrap();
            
        }
    };
    useEffect(() => {
      
        LoadEventOnPageEnd();
       
      
    }, []);

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
            {events.length > 0 ? events.map((event) => (
                <OrganiserEventCard event={event} />
            )) : <h1>No events to show here</h1>}
           

        </div>
    );
};

export default InfiniteOrganisationPastEvents;
