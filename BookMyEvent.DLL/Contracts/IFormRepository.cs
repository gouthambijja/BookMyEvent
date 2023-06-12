using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    /// <summary>
    /// This interface is used to perform all the database operations with respect to the Form model/Table
    /// </summary>
    public interface IFormRepository
    {
        /// <summary>
        /// This method is used to add a forms record into the data base.It takes one parameter as input which is a form object.
        /// </summary>
        /// <param name="form"></param>
        /// <returns>form model if success , NULL if there's any exception</returns>
        Task<Form> Add(Form form);
        /// <summary>
        /// To update form fields which includes formName, CreatedBy.This method takes one parameter which is updated form.
        /// </summary>
        /// <param name="Form"></param>
        /// <returns>form model if success , NULL if there's any exception</returns>
        Task<Form> Update(Form form);
        /// <summary>
        ///This method is used to delete a form record from the data base.It takes one parameter as input which is a formId.
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns>form model if success , NULL if there's any exception</returns>
        Task<bool> Delete(Guid formId);
        /// <summary>
        /// To get a form using form id.It takes formId as a parameter.
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns>it returns true if the Deletion is succesful</returns>
        Task<Form> Get(Guid formId);
        /// <summary>
        /// This method returns all the forms of a perticular organisation.It takes one parameter which is OrganisactionId
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <returns>Form model if success , NULL if there's any exception</returns>
        Task<List<Form>> GetByOrganisationId(Guid OrganisationId);
        /// <summary>
        /// This method returns all the forms of a particular 
        /// </summary>
        /// <param name="CreatorId"></param>
        /// <returns>Form Model if success , NULL if there's any exception</returns>
        Task<List<Form>> GetByCreatorId(Guid creatorId);

        /// <summary>
        /// This method is used to get all the forms of an organisation.It takes one parameter as input which is an organisationId.
        /// </summary>
        /// <param name="formId"></param>
        /// <returns>
        /// Returns a tuple of bool saying isForm deleted successfully or not and a string message
        /// </returns>
        Task<(bool IsActiveToggled, string Message)> UpdateIsActive(Guid formId);

    }
}
