using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    /// <summary>
    /// This interface is used to perform all the database operations with respect to the RegistrationFormField model/Table
    /// </summary>
    public interface IRegistrationFormFieldRepository
    {
        /// <summary>
        /// this method is used to add a new registrationFormField record into the RegistrationFormField table
        /// </summary>
        /// <param name="registrationFormField"></param>
        /// <returns>It returs RegistrationFormField if success,Null if there's any exception</returns>
        Task<RegistrationFormField> Add(RegistrationFormField registrationFormField);
        /// <summary>
        /// this method is used to delete registrationFormField record from the RegistrationFormField table.
        /// </summary>
        /// <param name="registrationFormFieldId"></param>
        /// <returns>It returs newly added RegistrationFormField if success,Null if there's any exception</returns>
        Task<bool> Delete(Guid registrationFormFieldId);
        /// <summary>
        /// this method is used to get registrationFormField record from the RegistrationFormField table
        /// </summary>
        /// <param name="registrationFormFieldId"></param>
        /// <returns>It returs true if success,false if there's any exception</returns>
        Task<RegistrationFormField> Get(Guid registrationFormFieldId);
        /// <summary>
        /// this method is used to update a registrationFormField record in the RegistrationFormField table
        /// </summary>
        /// <param name="registrationFormField"></param>
        /// <returns>It returs updated RegistrationFormField if success,Null if there's any exception</returns>
        Task<RegistrationFormField> Update(RegistrationFormField registrationFormField);
        /// <summary>
        /// this method is used to get all the registrationFormField records which has formId as given FormId parameter
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns>It returs RegistrationFormField if success,Null if there's any exception</returns>
        Task<List<RegistrationFormField>> GetAllFields(Guid FormId);
        /// <summary>
        /// this method is used to add a multiple registrationFormField records into the RegistrationFormField table
        /// </summary>
        /// <param name="registrationFormFieldList"></param>
        /// <returns>It returs true if success,false if there's any exception</returns>
        Task<bool> AddMany(List<RegistrationFormField> registrationFormFieldList);
    }
}
