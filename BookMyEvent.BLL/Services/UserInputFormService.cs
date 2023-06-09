using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Services
{
    public class UserInputFormService:IUserInputFormService
    {
        private readonly IUserInputFormRepository _userInputForm;
        private readonly IUserInputFormFieldsRepository _userInputFormField;

        public UserInputFormService(IUserInputFormRepository userInputForm, IUserInputFormFieldsRepository userInputFormField)
        {
            _userInputForm = userInputForm;
            _userInputFormField = userInputFormField;
        }

        public async Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>?> SubmitUserInputForm(List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> userForms)
        {
            try
            {
                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)> newFormsBL = new List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>();
                int totalForms = userForms.Count;
                for (int i = 0; i < totalForms; i++)
                {
                    var SingleForm = userForms[i];
                    var mapper = Automapper.InitializeAutomapper();
                    UserInputForm DLInputForm = mapper.Map<UserInputForm>(SingleForm.userInputForm);

                    UserInputForm newInputForm = await _userInputForm.Add(DLInputForm);
                    if(newInputForm == null) { return null; }
                    BLUserInputForm newUserInputFormBL = mapper.Map<BLUserInputForm>(newInputForm);
                    List<UserInputFormField>? DLUserInputFormFields = new List<UserInputFormField>();
                    foreach (var userInputFormField in SingleForm.userInputFormFields)
                    {
                        userInputFormField.UserInputFormId = newUserInputFormBL.UserInputFormId;
                        UserInputFormField DLUserInputFormField = mapper.Map<UserInputFormField>(userInputFormField);
                        DLUserInputFormFields.Add(DLUserInputFormField);
                    }
                    List<UserInputFormField>? DLNewUserInputFormFields = await _userInputFormField.AddMany(DLUserInputFormFields);
                    if(DLNewUserInputFormFields == null) { return null; }
                    List<BLUserInputFormField>? BLNewUserInputFormFields = new List<BLUserInputFormField>();

                    foreach (var DLNewuserInputFormField in DLNewUserInputFormFields)
                    {
                        BLUserInputFormField DLUserInputFormField = mapper.Map<BLUserInputFormField>(DLNewuserInputFormField);
                        BLNewUserInputFormFields.Add(DLUserInputFormField);
                    }
                    (BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields) BLNewSingleForm;
                    BLNewSingleForm.userInputForm = newUserInputFormBL;
                    BLNewSingleForm.UserInputFormFields = BLNewUserInputFormFields;
                    newFormsBL.Add(BLNewSingleForm);
                }
                return newFormsBL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<List<BLUserInputFormField>>?> GetUserFormsOfUserIdByEventId(Guid userId, Guid EventId)
        {
            try
            {
                var mapper = Automapper.InitializeAutomapper();

                List<Guid>? inputFormIds = await _userInputForm.GetInputFormIdByUserIdAndEventId(userId, EventId);
                if (inputFormIds == null) return null;
                List<List<BLUserInputFormField>> BLUserInputFormFieldsLists = new List<List<BLUserInputFormField>>();
                foreach (var id in inputFormIds)
                {
                    List<UserInputFormField> DLUserinputFormFields = await _userInputFormField.GetAllFieldsByFormId(id);
                    if(DLUserinputFormFields== null) return null;
                    List<BLUserInputFormField> BLUserInputFormFields=new List<BLUserInputFormField>();
                    foreach (var field in DLUserinputFormFields)
                    {
                        BLUserInputFormField bLUserInputFormField=mapper.Map<BLUserInputFormField>(field);
                        BLUserInputFormFields.Add(bLUserInputFormField);

                    }
                    BLUserInputFormFieldsLists.Add(BLUserInputFormFields);
                }
                return BLUserInputFormFieldsLists;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>?> GetAllUserFormsByEventId(Guid eventId)
        {
            try
            {
                var mapper = Automapper.InitializeAutomapper();
                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)> newFormsBL = new List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>();

                List<UserInputForm>? DLuserInputForms = await _userInputForm.GetUserInputFormsByEventId(eventId);
                if (DLuserInputForms == null) return null;
                foreach(var inputForm in DLuserInputForms)
                {
                    List<UserInputFormField> DLUserInputFormFields = await _userInputFormField.GetAllFieldsByFormId(inputForm.UserInputFormId);
                    if(DLUserInputFormFields == null) return null;
                    BLUserInputForm bLUserInputForm = mapper.Map<BLUserInputForm>(inputForm);
                    List<BLUserInputFormField> BLUserInputFormFields = new List<BLUserInputFormField>();
                    foreach(var field in DLUserInputFormFields)
                    {
                        BLUserInputFormField BLField = mapper.Map<BLUserInputFormField>(field);
                        BLUserInputFormFields.Add(BLField);
                    }
                    (BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields) BLNewSingleForm;
                    BLNewSingleForm.userInputForm = bLUserInputForm;
                    BLNewSingleForm.UserInputFormFields = BLUserInputFormFields;
                    newFormsBL.Add(BLNewSingleForm);
                }
                return newFormsBL;
            }
            catch
            {
                return null;
            }
        }



    }
}
