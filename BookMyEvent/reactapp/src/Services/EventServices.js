import AxiosPrivate from "../Hooks/AxiosPrivate";

const apiBase = "/api/Event";

const EventServices = () =>{
const Axios = AxiosPrivate();

const addNewEvent = async (newEvent) => {
    const response = await Axios.post(`${apiBase}/addevent`, newEvent, {
        headers: { "Content-Type": "multipart/form-data" },
        withCredentials: true,
    });
    return response.data;
};

const updateEvent = async (newEvent) => {
    const response = await Axios.put(`${apiBase}/updateevent`, newEvent, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    console.log(response.data);
    return response.data;
};

const getAllActivePublishedEvents = async (pageNumber = 1, pageSize = 10) => {
    const response = await Axios.get(`${apiBase}/GetAllActivePublishedEvents?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};
const updateEventRegistrationStatus = async (_event) => {
    try {
        const response = await Axios.put(`/api/events/${_event.eventId}/RegistrationStatus`, _event);
        return response.data;
    } catch (error) {
        // Handle error
        console.error('Error updating event registration status:', error);
        throw error;
    }
};

const updateIsCancelledEvent = async (_event) => {
    try {
        const response = await Axios.put(`/api/events/${_event.eventId}/CancelEvent`, _event);
        return response.data;
    } catch (error) {
        // Handle error
        console.error('Error updating event cancellation status:', error);
        throw error;
    }
};

const updateIsPublishedEvent = async (_event) => {
    try {
        const response = await Axios.put(`/api/events/${_event.eventId}/Publish`, _event);
        return response.data;
    } catch (error) {
        // Handle error
        console.error('Error updating event publication status:', error);
        throw error;
    }
};

const updateAcceptedBy = async (_event) => {
    try {
        const response = await Axios.put(`/api/Event/${_event.eventId}/Accept`, _event);
        return response.data;
    } catch (error) {
        // Handle error
        console.error('Error updating acceptedBy:', error);
        throw error;
    }
};

const updateRejectedBy = async (_event) => {
    try {
        const response = await Axios.put(`/api/Event/${_event.eventId}/Reject`, _event);
        return response.data;
    } catch (error) {
        // Handle error
        console.error('Error updating rejectedBy:', error);
        throw error;
    }
};

const getAllCreatedEventsByOrganisation = async (orgId) => {
    const response = await Axios.get(`${apiBase}/GetAllCreatedEventsByOrganisation?orgId=${orgId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getAllCreatedEventsByOrganiser = async (organiserId) => {
    const response = await Axios.get(`${apiBase}/GetAllCreatedEventsByOrganiser?organiserId=${organiserId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getFilteredEvents = async (filterEvent) => {
    const response = await Axios.post(`${apiBase}/GetFilteredEvents`, { ...filterEvent }, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    console.log(response.data);
    return response.data;
};

const deleteEvent = async (eventId, deletedBy) => {
    const response = await Axios.delete(`${apiBase}/${eventId}/deletedBy/${deletedBy}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getEventById = async (eventId) => {
    const response = await Axios.get(`${apiBase}/geteventbyid?eventId=${eventId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getOrganisationPastEvents = async (organisationId, pageNumber, pageSize) => {
    const response = await Axios.get(`${apiBase}/OrganisationPastEvents/${organisationId}?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getOrganiserPastEvents = async (organiserId, pageNumber, pageSize) => {

    const response = await Axios.get(`${apiBase}/OrganiserPastEvents/${organiserId}?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getOrganiserEventRequests = async (organiserId) => {
    const response = await Axios.get(`${apiBase}/OrganiserRequests/${organiserId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
}

const getOrganisationEventRequests = async (organisationId) => {
    const response = await Axios.get(`${apiBase}/OrganisationRequests/${organisationId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
}

const getEventImages = async (eventId) => {
    const response = await Axios.get(`${apiBase}/geteventimages/${eventId}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

return {
    addNewEvent,
    updateEvent,
    getAllActivePublishedEvents,
    updateEventRegistrationStatus,
    updateIsCancelledEvent,
    updateIsPublishedEvent,
    updateAcceptedBy,
    updateRejectedBy,
    getAllCreatedEventsByOrganisation,
    getAllCreatedEventsByOrganiser,
    getFilteredEvents,
    deleteEvent,
    getEventById,
    getEventImages,
    getOrganisationPastEvents,
    getOrganiserPastEvents,
    getOrganiserEventRequests,
    getOrganisationEventRequests
};
}
export default EventServices;