import React, { useState } from "react";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import store from "../App/store";
import { fetchOrganisationPastEvents } from "../Features/ReducerSlices/EventsSlice";
import OrganiserEventCard from "./OrganiserEventCards";

let cnt = 0;
const InfiniteOrganisationPastEvents = () => {

    var tempPageNo = useSelector((store) => store.events.organisationPastEventsPageNo);
    const [pageNumber, setPageNumber] = useState(tempPageNo);
    const [pageSize, setPageSize] = useState(10);
    const dispatch = useDispatch();
    var profInfo = useSelector((store) => store.profile.info);
    var paginationDetails = {
        organisationId: profInfo.organisationId,
        pageNumber : { pageNumber },
        pageSize : { pageSize }
    }
    //const isFetchedAlready = useSelector((store) => store.events.isMyPastEventsFetched);
    //if (isFetchedAlready) {
    //    dispatch(fetchOrganiserPastEvents(profInfo.administratorId));
    //}
    let events = useSelector((store) => store.events.organisationPastEvents);
    const LoadEventOnPageEnd = async () => {
        const scrollPosition = document.documentElement.scrollTop;
        const contentHeight = document.documentElement.scrollHeight;
        const viewportHeight = window.innerHeight;
        if (scrollPosition + viewportHeight >= contentHeight - 100) {
            await dispatch(fetchOrganisationPastEvents(paginationDetails));
            setPageNumber(pageNumber + 1);
        }
    };

    useEffect(() => {
        if (events.length==0){
            dispatch(fetchOrganisationPastEvents( {
                organisationId: profInfo.organisationId,
                pageNumber : 1,
                pageSize :10
            }))
        }
        window.addEventListener("scroll", LoadEventOnPageEnd);
        return () => {
            window.addEventListener("scroll", LoadEventOnPageEnd);
        };
    }, [pageNumber]);

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
