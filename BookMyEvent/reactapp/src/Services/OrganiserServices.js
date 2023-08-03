import AxiosPrivate from "../Hooks/AxiosPrivate";
import AxiosPublic from "../Api/Axios";
const apiBase = "/api/Organiser";

const OrganiserServices = () =>{
    const Axios = AxiosPrivate();

const getOrganiserById = async (id) => {
    const response = await Axios.get(`${apiBase}/${id}`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;

}
const checkEmail = async (email) => {
    const response = await Axios.get(`${apiBase}/IsEmailTaken?email=${email}`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;

}
const loginOrganiser = async (formData) => {

    const response = await AxiosPublic.post(`${apiBase}/loginOrganiser`,
        JSON.stringify(formData),
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }

    )
    return response.data.accessToken;
}

const addOrganiser = async (formData) => {
    //console.log(formData);
    const response = await Axios.post(`${apiBase}/CreateOrganiser`,
        formData,
        {
            headers: { 'Content-Type': 'multipart/form-data' },
            withCredentials: true
        }
    )
    return response.data;
}

const registerPeer = async (formData) => {
    //console.log(formData);
    const response = await AxiosPublic.post(`${apiBase}/RegisterPeer`,
        formData,
        {
            headers: { 'Content-Type': 'multipart/form-data' },
            withCredentials: true
        }

    )
    return response.data;
}

const addOwner = async (formData) => {
    //console.log(formData);
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

const getNoOfRequestedPeers = async (orgId) => {
    const response = await Axios.get(`${apiBase}/Organisation/${orgId}/NoOfRequestedPeers`, {
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

const rejectOrganiser = async (administrator) => {
    const response = await Axios.put(`${apiBase}/${administrator.administratorId}/reject`, administrator, {
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
    const response = await AxiosPrivate.get(`${apiBase}/IsEmailTaken?email=${email}`, {
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

return {
    getOrganiserById,
    loginOrganiser,
    addOrganiser,
    registerPeer,
    addOwner,
    getRequestedOwners,
    getRequestedPeers,
    acceptOrganiser,
    rejectOrganiser,
    updateOrganiser,
    deleteOrganiser,
    deleteAllOrganisationOrganisers,
    getOrganisationOrganisers,
    isEmailTaken,
    getAllOwners,
    checkEmail,
    getNoOfRequestedPeers,
};
}
export default OrganiserServices;
