import Axios from "../Api/Axios";
import store from "../App/store";

const apiBase = "/api/OrganiserForm";

const AddForm = async (formName) => {
  const auth = store.getState().auth;
  const organiser = store.getState().profile.info;
  const FieldTypes = store.getState().formFields.formFields;
  const FormFields = store.getState().EventRegistrationFormFields.inputFields;
  try {
    const RegistrationFormFields = [];
    const form = await Axios.post(
      `${apiBase}/AddForm`,
      {
        FormName: formName,
        OrganisationId: organiser.organisationId,
        createdBy: auth.id,
      },
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    const FormId = form.data;
    FormFields.forEach((e) => {
      RegistrationFormFields.push({
        RegistrationFormFieldId:FormId,
        FormId: FormId,
        FieldTypeId: FieldTypes.find((a) => a.type == e.FieldType).fieldTypeId,
        Lable: e.Label,
        Validations: JSON.stringify(e.Validations).toString(),
        Options:  JSON.stringify(e.Options).toString(),
        IsRequired: e.IsRequired,
      });
    });
    const IsFormFieldsInserted = await Axios.post(
      `${apiBase}/AddFormFields`,
      RegistrationFormFields
      ,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    return IsFormFieldsInserted.data;
  } catch (error) {
    throw new Error(error);
  }
};
const getFormById = async (formId) => {
  try {
    const response = await Axios.get(`${apiBase}/FormId?FormId=${formId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};
const IsFormNameTaken = async (formName) => {
  try {
    const response = await Axios.get(`${apiBase}/IsFormNameTaken/${formName}`);
    return response.data;
  } catch (e) {
    throw new Error(e.response.data);
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

export default {
  getFormById,
  getFieldTypesByFormId,
  getAllOrganizationForms,
  getAllFormsCreatedByOrganizer,
  deleteForm,
  IsFormNameTaken,
  AddForm,
};
