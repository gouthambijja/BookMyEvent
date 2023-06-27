import AxiosPrivate from "../Hooks/AxiosPrivate";
import { axiosPrivate } from "../Api/Axios";
const apiBase = "/api/Organisation";

const OrganisationService = () => {
  const Axios = AxiosPrivate();
  const getAllOrganisations = async (pageNumber, pageSize) => {
    const response = await Axios.get(
      `${apiBase}/?pageNumber=${pageNumber}&pageSize=${pageSize}`,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return response.data;
  };

  const getOrganisationById = async (id) => {
    const response = await Axios.get(`${apiBase}/${id}`, {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
    });
    return response.data;
  };
  const getOrganisationByName = async (orgName) => {
    const response = await Axios.get(`${apiBase}/getOrgIdByName/${orgName}`, {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
    });
    return response.data;
  };
  const IsOrgNameTaken = async (organisationName) => {
    const response = await axiosPrivate.get(
      `${apiBase}/CheckOrganisationName/${organisationName}`,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return response.data;
  };

  const updateOrganisation = async (updatedOrg) => {
    const response = await Axios.put(
      `${apiBase}/${updatedOrg.id}`,
      updatedOrg,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return response.data;
  };

  const deleteOrganisation = async (id) => {
    const response = await Axios.delete(`${apiBase}/${id}`, {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
    });
    return response.data;
  };

  return {
    getAllOrganisations,
    getOrganisationById,
    getOrganisationByName,
    IsOrgNameTaken,
    updateOrganisation,
    deleteOrganisation,
  };
};
export default OrganisationService;
