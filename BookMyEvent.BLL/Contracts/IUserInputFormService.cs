using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.BLL.Models;
namespace BookMyEvent.BLL.Contracts
{
    
    /// <summary>
    /// This interface consists of methods that are needed when an user registered for an event
    /// </summary>
    public interface IUserInputFormService
    {
        /// <summary>
        /// This method is used to submit the userInputForm when an User registers for an Event
        /// </summary>
        /// <param name="userForms">List of UserInputForms and UserInputFormFields</param>
        /// <returns> List of UserInputForms and UserInputFormFields </returns>
        public Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>> SubmitUserInputForm(List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> userForms);

        /// <summary>
        /// This method is used to get Registered User Details of an Event By UserID
        /// </summary>
        /// <param name="userId"> UserId of User who registered for an Event(with multiple users)</param>
        /// <param name="EventId"> EventId of an Event </param>
        /// <returns>User Details of Registered Users for an Event registered by a particular User</returns>
        public Task<List<List<BLUserInputFormField>>> GetUserFormsOfUserIdByEventId(Guid userId, Guid EventId);

        /// <summary>
        /// This method is used to get the details of All users registered for an Event
        /// </summary>
        /// <param name="eventId"> EventID of an Event </param>
        /// <returns> List of all Users registered for a particular event </returns>
        public Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>> GetAllUserFormsByEventId(Guid eventId);
    }

}
