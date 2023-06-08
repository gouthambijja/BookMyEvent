using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.BLL.Models;
namespace BookMyEvent.BLL.Contracts
{
    public interface IUserInputFormService
    {
       public Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>> SubmitUserInputForm(List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> userForms);

        public Task<List<List<BLUserInputFormField>>> GetUserFormsByEventId(Guid userId, Guid EventId);
        public Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>> GetAllUserFormsByEventId(Guid eventId);
    }

}
