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
        const response = await Axios.put(`/api/events/${_event.eventId}/Accept`, _event);
        return response.data;
    } catch (error) {
        // Handle error
        console.error('Error updating acceptedBy:', error);
        throw error;
    }
};

const updateRejectedBy = async (_event) => {
    try {
        const response = await Axios.put(`/api/events/${_event.eventId}/Reject`, _event);
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
    getFilteredEvents,
    deleteEvent,
    getEventById,
    getEventImages,
    getOrganisationPastEvents,
    getOrganiserPastEvents,
    getOrganiserEventRequests,
    getOrganisationEventRequests
};
//const getFilteredEvents = async (startDate, endDate, startPrice, endPrice, location, isFree, categoryIds, pageNumber = 1, pageSize = 10) => {
//    try {
//        startDate = startDate || new Date().toISOString();
//        endDate = endDate || new Date().toISOString();
//        startPrice = startPrice || 0;
//        endPrice = endPrice || Number.MAX_VALUE;
//        location = location || "";
//        isFree = isFree || false;
//        categoryIds = categoryIds || [];

//        const response = await Axios.get(`${apiBase}/GetFilteredEvents`, {
//            params: {
//                startDate: startDate,
//                endDate: endDate,
//                startPrice: startPrice,
//                endPrice: endPrice,
//                location: location,
//                isFree: isFree,
//                categoryIds: categoryIds,
//                pageNumber: pageNumber,
//                pageSize: pageSize
//            },
//            withCredentials: true
//        });

//        return response.data;
//    } catch (error) {
//        throw error;
//    }
//}

// export default {
//     addNewEvent,
//     updateEvent,
//     getAllActivePublishedEvents,
//     updateEventRegistrationStatus,
//     updateIsCancelledEvent,
//     updateIsPublishedEvent,
//     updateAcceptedBy,
//     updateRejectedBy,
//     getAllCreatedEventsByOrganisation,
//     getAllCreatedEventsByOrganiser,
//     getFilteredEvents,
//     deleteEvent,
//     getEventById,
// };


// import Axios from "../Api/Axios";

// const GetAllActivePublishedEvents = async () => {
//   const response = await Axios.get(`api/Event/GetAllActivePublishedEvents`, {
//     headers: { "content-type": "application/json" },
//     withCredentials: true,
//   });
//   return response.data;
// };
// const GetEventById = async (EventId) => {
//   const response = await Axios.get(
//     `api/Event/geteventbyid?eventId=${EventId}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByCategoryId = async (categoryId) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByCategoryId?categoryId=${categoryId}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByOrgId = async (OrgId) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByOrgId?orgId=${OrgId}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByLocation = async (Location) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByLocation?location=${Location}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByStartDate = async (startDate) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByStartdate?date=${startDate}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByEndDate = async (endDate) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByEndDate?date=${endDate}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByStartDateandEndDate = async (
//   startDate,
//   endDate
// ) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByStartDateandEndDate?startDate=${startDate}&endDate=${endDate}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsHavingMoreThanPrice = async (Price) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsHavingMoreThanPrice?startingPrice=${Price}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsHavingLessThanPrice = async (Price) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsHavingLessThanPrice?endingPrice=${Price}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsHavingPricerange = async (
//   startingPrice,
//   endingPrice
// ) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsHavingPriceRange?startingPrice=${startingPrice}&endingPrice=${endingPrice}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsHavingName = async (EventName) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsHavingName?name=${EventName}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByMode = async (IsOnline) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByMode?isOffline=${IsOnline}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedIsFreeEvents = async (IsFree) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedIsFreeEvents?isFree=${IsFree}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllActivePublishedEventsByIsPublished = async (IsPublished) => {
//   const response = await Axios.get(
//     `api/Event/GetAllActivePublishedEventsByIsPublished?isPublished=${IsPublished}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };

// const UpdateEventRegistrationStatus = async (
//   EventId,
//   registrationstatusId,
//   updatedBy,
//   UpdatedAt
// ) => {
//   const response = await Axios.get(
//     `api/Event/UpdateEventRegistrationStatus?eventId=${EventId}&registrationStatusId=${registrationstatusId}&updatedBy=${updatedBy}&updatedAt=${UpdatedAt}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const UpdateIsCancelled = async (EventId, updatedBy, UpdatedAt) => {
//   const response = await Axios.get(
//     `api/Event/UpdateIsCancelled?eventId=${EventId}&updatedBy=${updatedBy}&updatedAt=${UpdatedAt}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const UpdateIsPublishedEvent = async (EventId, updatedBy, UpdatedAt) => {
//   const response = await Axios.get(
//     `api/Event/UpdateIsPublished?eventId=${EventId}&updatedBy=${updatedBy}&updatedAt=${UpdatedAt}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const UpdateAcceptedBy = async (EventId, AcceptedBy, UpdatedBy, UpdatedAt) => {
//   const response = await Axios.get(
//     `api/Event/UpdateAcceptedBy?eventId=${EventId}&acceptedBy=${AcceptedBy}&updatedBy=${UpdatedBy}&updatedAt=${UpdatedAt}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const UpdateRejectedBy = async (EventId, RejectedBy, UpdatedBy, UpdatedAt) => {
//   const response = await Axios.get(
//     `api/Event/UpdateRejectedBy?eventId=${EventId}&rejectedBy=${RejectedBy}&updatedBy=${UpdatedBy}&updatedAt=${UpdatedAt}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllCreatedEventsByOrganization = async (OrgId) => {
//   const response = await Axios.get(
//     `api/Event/GetAllEventsCreatedEventsByOrganisation?orgId=${OrgId}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// const GetAllCreatedEventsByOrganiser = async (OrganiserId) => {
//   const response = await Axios.get(
//     `api/Event/GetAllCreatedEventsByOrganiser?organiserId=${OrganiserId}`,
//     {
//       headers: { "content-type": "application/json" },
//       withCredentials: true,
//     }
//   );
//   return response.data;
// };
// export {
//   GetAllActivePublishedEventsByOrgId,
//   GetAllCreatedEventsByOrganization,
//   GetAllCreatedEventsByOrganiser,
//   UpdateRejectedBy,
//   UpdateAcceptedBy,
//   UpdateIsPublishedEvent,
//   UpdateIsCancelled,
//   UpdateEventRegistrationStatus,
//   GetAllActivePublishedEventsByIsPublished,
//   GetAllActivePublishedIsFreeEvents,
//   GetAllActivePublishedEventsByMode,
//   GetAllActivePublishedEventsHavingName,
//   GetAllActivePublishedEventsHavingPricerange,
//   GetAllActivePublishedEventsHavingLessThanPrice,
//   GetAllActivePublishedEventsHavingMoreThanPrice,
//   GetAllActivePublishedEvents,
//   GetEventById,
//   GetAllActivePublishedEventsByCategoryId,
//   GetAllActivePublishedEventsByLocation,
//   GetAllActivePublishedEventsByStartDate,
//   GetAllActivePublishedEventsByEndDate,
//   GetAllActivePublishedEventsByStartDateandEndDate,
// };
