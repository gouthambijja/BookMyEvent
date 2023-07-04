import React from 'react';
import EventsFilter from "../Components/EventsFilter"

const EventsPage = () => {
    const handleFilter = (filter) => {
        //console.log(filter);
    };
    return (
        <div>
            <div>
                <h2>Parent Component</h2>
                <EventsFilter onFilter={handleFilter} />
            </div>
        </div>
    );
}

export default EventsPage;
