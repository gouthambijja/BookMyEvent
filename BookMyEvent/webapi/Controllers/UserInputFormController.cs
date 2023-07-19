using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserInputFormController : ControllerBase
    {
        private readonly IUserInputFormService _userInputFormService;
        private readonly FileLogger _fileLogger;
        public UserInputFormController(IUserInputFormService userInputFormService, FileLogger fileLogger)
        {
            _userInputFormService = userInputFormService;
            _fileLogger = fileLogger;
        }
        /// <summary>
        /// Service To Add user input Form
        /// </summary>
        /// <param name="InputUserForms"></param>
        /// <returns>List of User form fields and those values</returns>
        [HttpPost("submitform")]
        public async Task<IActionResult> SubmitUserInputForm([FromBody] List<SubmitUserInputForm?> InputUserForms)
        {
            try
            {

                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> userForms = new List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)>();
                foreach (var _userInputForm in InputUserForms)
                {
                    (BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields) SingleForm;
                    SingleForm.userInputForm = _userInputForm.UserInputFormBL;
                    SingleForm.userInputFormFields = _userInputForm.UserInputFormFields;
                    userForms.Add(SingleForm);
                }

                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> newUserForms = await _userInputFormService.SubmitUserInputForm(userForms);
                if (newUserForms == null) { _fileLogger.AddExceptionToFile("[SubmitUserInputForm] Submitting UserInputForm FirstBadRequest"); return BadRequest("Error in Business layer"); }
                List<SubmitUserInputForm> newInputUserForm = new List<SubmitUserInputForm>();
                foreach (var form in newUserForms)
                {
                    SubmitUserInputForm _form = new SubmitUserInputForm();
                    _form.UserInputFormBL = form.userInputForm;
                    _form.UserInputFormFields = form.userInputFormFields;
                    newInputUserForm.Add(_form);
                }
                _fileLogger.AddInfoToFile("[SubmittingUserInputForm] SubmittingUserInputForm Success");
                return Ok(newInputUserForm);
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[SubmittingUserInputForm] SubmittingUserINputForm Failure Badrequest second");
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to get user forms of User By Event id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="EventId"></param>
        /// <returns>List of User Forms of User</returns>
        [HttpGet("GetUserFormsOfUserIdByEventId")]

        public async Task<IActionResult> GetUserFormsOfUserIdByEventId(Guid userId, Guid EventId)
        //public async Task<List<List<BLUserInputFormField>>?> GetUserFormsOfUserIdByEventId(Guid userId, Guid EventId)
        {
            try
            {
                List<List<BLUserInputFormField>> userForms = await _userInputFormService.GetUserFormsOfUserIdByEventId(userId, EventId);
                if (userForms == null)
                {
                    _fileLogger.AddExceptionToFile("[GetFormsofUserIdByEventId] Getting Forms of userIdNyEventIf Badrequest"); return BadRequest("Error in BL");
                }
                _fileLogger.AddInfoToFile("[GetUserFormsofUSerIdByEventId] GettingUSerForms Success ");
                return Ok(userForms);
            }
            catch
            {
                _fileLogger.AddExceptionToFile("[GetUserFormsofUserIdByEventID] Getting UserForms Failure badrequest");
                return BadRequest("Error in controller");
            }
        }
        /// <summary>
        /// Service to get All the User forms by event Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>list of user forms </returns>
        [HttpGet("GetAllUserFormsByEventId")]
        public async Task<IActionResult> GetAllUserFormsByEventId(Guid eventId)
        //public async Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>?> GetAllUserFormsByEventId(Guid eventId)
        {
            try
            {
                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)> userForms = await _userInputFormService.GetAllUserFormsByEventId(eventId);
                if (userForms == null) { _fileLogger.AddExceptionToFile("[GetAllUserFormsByEventId] Getting All UserFormsByEventID Failure"); return BadRequest("Error in BL"); }
                _fileLogger.AddInfoToFile("[GetAllUserFormsByEventId] Getting All UserForms Success");
                return Ok(userForms);
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetAllUserFormsByEventID] Getting All USerForms BadRequest");
                return BadRequest(ex.Message);

            }
        }




    }
}
