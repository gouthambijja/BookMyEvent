using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    /// <summary>
    ///     This interface is used to perform Database operations on UserInputFormRepository
    /// </summary>
    public interface IUserInputFormRepository
    {   
        /// <summary>
        /// This method is used to add UserInputForm in to the table
        /// </summary>
        /// <param name="inputForm"></param>
        /// <returns>returns UserInputForm on success and NUll on Exception</returns>
        Task<UserInputForm> Add(UserInputForm inputForm);
        /// <summary>
        /// This method is used to Update UserInputForm
        /// </summary>
        /// <param name="inputForm"></param>
        /// <returns>returns UserInputForm on successful Updation , Null on Exception or failure</returns>
        Task<UserInputForm> Update(UserInputForm inputForm);
        /// <summary>
        /// This method is used to Delete a record from the UserInputForms Table
        /// </summary>
        /// <param name="inputFormId"></param>
        /// <returns>return true if success , false if exception or failure</returns>
        Task<bool> Delete(Guid inputFormId);
        /// <summary>
        /// This method is used to Get UserInputForm by using inputFormId and eventId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns>It returns UserInputForm on success  , Null on Exception or failure</returns>
        Task<List<Guid>?> GetInputFormIdByUserIdAndEventId(Guid userId, Guid eventId);

        /// <summary>
        /// This method is used to Get UserInputForms by using eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>It returns UserInputForm on success  , Null on Exception or failure</returns>
        Task<List<UserInputForm>?> GetUserInputFormsByEventId(Guid eventId);
    }
}
