import Axios from "../Api/Axios";

const apiBase = "/api/OrganiserForm";

const getFormById = async (formId) => {
  try {
    const response = await Axios.get(`${apiBase}/FormId?FormId=${formId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

const getFieldTypesByFormId = async (formId) => {
  try {
    const response = await Axios.get(
      `${apiBase}/FieldTypesByFormId?FormId=${formId}`
    );
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

const getAllOrganizationForms = async (orgId) => {
  try {
    const response = await Axios.get(
      `${apiBase}/OrganizationForms?OrgId=${orgId}`
    );
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

const getAllFormsCreatedByOrganizer = async (organizerId) => {
  try {
    const response = await Axios.get(
      `${apiBase}/OrganizerForms?OrganizerId=${organizerId}`
    );
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

const deleteForm = async (formId) => {
  try {
    const response = await Axios.delete(
      `${apiBase}/DeleteForm?FormId=${formId}`
    );
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export default{
  getFormById,
  getFieldTypesByFormId,
  getAllOrganizationForms,
  getAllFormsCreatedByOrganizer,
  deleteForm,
};
