import Axios from "../Api/Axios";

const apiBase = "/api/Event";

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
    return response.data;
};

const getAllActivePublishedEvents = async (pageNumber = 1, pageSize = 10) => {
    const response = await Axios.get(`${apiBase}/GetAllActivePublishedEvents?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const updateEventRegistrationStatus = async (eventId, registrationStatusId, updatedBy, updatedAt) => {
    const response = await Axios.get(`${apiBase}/UpdateEventRegistrationStatus?eventId=${eventId}&registrationStatusId=${registrationStatusId}&updatedBy=${updatedBy}&updatedAt=${updatedAt}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const updateIsCancelledEvent = async (eventId, updatedBy, updatedAt) => {
    const response = await Axios.get(`${apiBase}/UpdateIsCancelledEvent?eventId=${eventId}&updatedBy=${updatedBy}&updatedAt=${updatedAt}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const updateIsPublishedEvent = async (eventId, updatedBy, updatedAt) => {
    const response = await Axios.get(`${apiBase}/UpdateIsPublishedEvent?eventId=${eventId}&updatedBy=${updatedBy}&updatedAt=${updatedAt}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const updateAcceptedBy = async (eventId, acceptBy, updatedBy, updatedAt) => {
    const response = await Axios.get(`${apiBase}/UpdateAcceptedBy?eventId=${eventId}&acceptBy=${acceptBy}&updatedBy=${updatedBy}&updatedAt=${updatedAt}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const updateRejectedBy = async (eventId, rejectedBy, updatedBy, updatedAt) => {
    const response = await Axios.get(`${apiBase}/UpdateRejectedBy?eventId=${eventId}&rejectedBy=${rejectedBy}&updatedBy=${updatedBy}&updatedAt=${updatedAt}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
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

const getAllActivePublishedEventsByFilter = async (pageNumber = 1, pageSize = 10, filter) => {
    const response = await Axios.get(`${apiBase}/GetAllActivePublishedEventsByFilter?pageNumber=${pageNumber}&pageSize=${pageSize}&filter=${filter}`, {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
    });
    return response.data;
};

const getFilteredEvents = async (startDate, endDate, startPrice, endPrice, location, isFree, categoryIds, pageNumber = 1, pageSize = 10) => {
    try {
        startDate = startDate || new Date().toISOString();
        endDate = endDate || new Date().toISOString();
        startPrice = startPrice || 0;
        endPrice = endPrice || Number.MAX_VALUE;
        location = location || "";
        isFree = isFree || false;
        categoryIds = categoryIds || [];

        const response = await Axios.get(`${apiBase}/GetFilteredEvents`, {
            params: {
                startDate: startDate,
                endDate: endDate,
                startPrice: startPrice,
                endPrice: endPrice,
                location: location,
                isFree: isFree,
                categoryIds: categoryIds,
                pageNumber: pageNumber,
                pageSize: pageSize
            },
            withCredentials: true
        });

        return response.data;
    } catch (error) {
        throw error;
    }
}

export default {
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
    getFilteredEvents
};
