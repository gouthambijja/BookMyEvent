using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Contracts
{
    public interface IOrganiserFormServices
    {
        /// <summary>
        /// An asynchronous method that gets the form by id
        /// </summary>
        /// <param name="id">
        /// Takes in the form id as a Guid type
        /// </param>
        /// <returns>
        /// Returns the form as a BLForm object
        /// </returns>
        Task<BLForm> GetFormById(Guid id);


        /// <summary>
        /// An asynchronous method that gets all the forms with their fields of an organisation
        /// </summary>
        /// <param name="id">
        /// Takes in the organisation id as a Guid type
        /// </param>
        /// <returns>
        /// Returns a list of tuple of BLForm and a list of BLRegistrationFormFields
        /// </returns>
        Task<List<BLForm>> GetAllOrganisationForms(Guid id);

        /// <summary>
        /// An asynchronous method that deletes a form
        /// </summary>
        /// <param name="id">
        /// Takes in the form id as a Guid type
        /// </param>
        /// <returns>
        /// Returns a tuple of bool saying isForm deleted successfully or not and a string message
        /// </returns>
        Task<(bool IsFormDeleted, string Message)> DeleteForm(Guid id);

        /// <summary>
        /// An asynchronous method that gets all the forms created by an organiser
        /// </summary>
        /// <param name="id">
        /// Takes in the organiser id as a Guid type
        /// </param>
        /// <returns>
        /// Returns a list of tuple of BLForm and a list of BLRegistrationFormFields
        /// </returns>
        Task<List<(BLForm form, List<BLRegistrationFormFields> formFields)>> GetAllFormsCreatedBy(Guid id);

        /// <summary>
        /// An asynchronous method that adds a form
        /// </summary>
        /// <param name="form">
        /// Takes in the form as a BLForm object
        /// </param>
        /// <returns>
        /// Returns a tuple of Form Id as Guid and a string message
        /// </returns>
        Task<(Guid FormId, string Message)> AddForm(BLForm form);

        /// <summary>
        /// An asynchronous method that gets all the form fields of a form
        /// </summary>
        /// <param name="id">
        /// Takes in the form id as a Guid type
        /// </param>
        /// <returns>
        /// Returns a list of BLRegistrationFormFields objects
        /// </returns>

        Task<List<BLRegistrationFormFields>> GetFormFieldsByFormId(Guid id);

        /// <summary>
        /// An asynchronous method that gets all the field types
        /// </summary>
        /// <returns>
        /// Returns a list of BLFieldType objects
        /// </returns>
        Task<List<BLFieldType>> GetFieldTypes();
        /// <summary>
        /// checks whether the form name is available or not
        /// </summary>
        /// <param name="formName"></param>
        /// <returns>return true if available ,else false</returns>
        Task<bool> IsformNameTaken(string formName);
        /// <summary>
        /// adds multiples form fields to the repository
        /// </summary>
        /// <param name="registrationFormFields"></param>
        /// <returns>return true if success else false</returns>
        public Task<bool> AddRegistrationFormFields(List<BLRegistrationFormFields> registrationFormFields);

    }
}
