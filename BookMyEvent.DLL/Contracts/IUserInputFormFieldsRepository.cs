using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    /// <summary>
    /// This interface is used perform DataBase operations on  UserInputFormFields Model/table
    /// </summary>
    public interface IUserInputFormFieldsRepository
    {
        /// <summary>
        /// This method is used to add UserinputFormField into the table
        /// </summary>
        /// <param name="formField"></param>
        /// <returns>It returns UserInputFormField on Succesful operation and Null on failure</returns>
        Task<UserInputFormField> Add(UserInputFormField formField);
        /// <summary>
        /// This method is used to add multiple UserinputFormFields into the table
        /// </summary>
        /// <param name="formFields"></param>
        /// <returns>Returns Added UserInputFormFields on successful operation and null on Exception</returns>
        Task<List<UserInputFormField>?> AddMany(List<UserInputFormField> formFields);
        /// <summary>
        /// This method is used to update the userInputFormField in the DataBase
        /// </summary>
        /// <param name="formField"></param>
        /// <returns>returns updated UserInputFormField on Success, Null on Exception or any failure</returns>
        Task<UserInputFormField> Update(UserInputFormField formField);
        /// <summary>
        /// This method is used to delete a UserInputFormField by using its Id
        /// </summary>
        /// <param name="UserInputFormFieldId"></param>
        /// <returns>returns true on success and false on failure</returns>
        Task<bool> Delete(Guid UserInputFormFieldId);
        /// <summary>
        /// This method is used to Get All the UserInputFormFields By using UserInputFormId(i.e., getting all the form fields by using its Id)
        /// </summary>
        /// <param name="UserInputFormId"></param>
        /// <returns>It returns the List of UserInputFormFields on success , Null on Exception</returns>
        Task<List<UserInputFormField>> GetAllFieldsByFormId(Guid UserInputFormId);
    }
}
