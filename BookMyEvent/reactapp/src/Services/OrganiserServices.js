import Axios from "../Api/Axios";

const apiBase = "/api/Organiser";
const getOrganiserById = async (id) => {
    const response = await Axios.get(`${apiBase}/${id}`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;

}

const loginOrganiser = async (formData) => {

    const response = await Axios.post(`${apiBase}/loginOrganiser`,
        JSON.stringify(formData),
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }

    )
    return response.data.accessToken;
}

const addOrganiser = async (formData) => {
    console.log(formData);
    const response = await Axios.post(`${apiBase}/CreateOrganiser`,
        formData,
        {
            headers: { 'Content-Type': 'multipart/form-data' },
            withCredentials: true
        }
    )
    return response.data;
}

const addPeer = async (formData) => {
    console.log(formData);
    const response = await Axios.post(`${apiBase}/RegisterPeer`,
        formData,
        {
            headers: { 'Content-Type': 'multipart/form-data' },
            withCredentials: true
        }

    )
    return response.data;
}

const addOwner = async (formData) => {
    console.log(formData);
    const response = await Axios.post(`${apiBase}/RegisterOwner`,
        formData,
        {
            headers: { 'Content-Type': 'multipart/form-data' },
            withCredentials: true
        }

    )
    return response.data;
}

const getRequestedOwners = async () => {
    const response = await Axios.get(`${apiBase}/RequestedOwners`, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const getRequestedPeers = async (orgId) => {
    const response = await Axios.get(`${apiBase}/Organisation/${orgId}/RequestedPeers`, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const acceptOrganiser = async (administrator) => {
    const response = await Axios.put(`${apiBase}/${administrator.administratorId}/Accept`, administrator, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const updateOrganiser = async (administrator) => {
    const response = await Axios.put(`${apiBase}/${administrator.administratorId}`, administrator, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const deleteOrganiser = async (id, deletedById) => {
    const response = await Axios.delete(`${apiBase}/${id}/DeleteBy/${deletedById}`, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const deleteAllOrganisationOrganisers = async (id, blockerId) => {
    const response = await Axios.delete(`${apiBase}/Organisation/${id}/BlockedBy/${blockerId}`, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const getOrganisationOrganisers = async (id) => {
    const response = await Axios.get(`${apiBase}/Organisation/${id}`, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const isEmailTaken = async (email) => {
    const response = await Axios.get(`${apiBase}/IsEmailTaken?email=${email}`, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

const getAllOwners = async () => {
    const response = await Axios.get(`${apiBase}/AllOwners`, {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true,
    });
    return response.data;
};

export {
    getOrganiserById,
    loginOrganiser,
    addOrganiser,
    addPeer,
    addOwner,
    getRequestedOwners,
    getRequestedPeers,
    acceptOrganiser,
    updateOrganiser,
    deleteOrganiser,
    deleteAllOrganisationOrganisers,
    getOrganisationOrganisers,
    isEmailTaken,
    getAllOwners,
};
