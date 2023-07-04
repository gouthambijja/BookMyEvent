using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    public class UserInputFormController:ControllerBase
    {
        private readonly IUserInputFormService _userInputFormService;
        public UserInputFormController(IUserInputFormService userInputFormService)
        {
            _userInputFormService = userInputFormService;
        }
        /// <summary>
        /// Service To Add user input Form
        /// </summary>
        /// <param name="InputUserForms"></param>
        /// <returns>List of User form fields and those values</returns>
        [HttpPost("submitform")]
        public async Task<IActionResult> SubmitUserInputForm([FromBody] List<SubmitUserInputForm> InputUserForms)
        {
            try
            {
                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> userForms = new List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)>();
                foreach(var _userInputForm in InputUserForms)
                {
                    (BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields) SingleForm;
                    SingleForm.userInputForm = _userInputForm.UserInputFormBL;
                    SingleForm.userInputFormFields = _userInputForm.UserInputFormFields;
                    userForms.Add(SingleForm);
                }

                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> newUserForms= await _userInputFormService.SubmitUserInputForm(userForms);
                if(newUserForms==null) { return BadRequest("Error in Business layer"); }
                List<SubmitUserInputForm> newInputUserForm = new List<SubmitUserInputForm>();
                foreach(var form in newUserForms)
                {
                    SubmitUserInputForm _form=new SubmitUserInputForm();
                    _form.UserInputFormBL = form.userInputForm;
                    _form.UserInputFormFields=form.userInputFormFields;
                    newInputUserForm.Add(_form);
                }

                return Ok(newInputUserForm);
            }
            catch
            {
                return BadRequest("Error occured in controller");
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
                List<List<BLUserInputFormField>> userForms= await _userInputFormService.GetUserFormsOfUserIdByEventId(userId, EventId);
                if(userForms==null) { return BadRequest("Error in BL"); }
                return Ok(userForms);
            }
            catch
            {
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
                List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)> userForms= await _userInputFormService.GetAllUserFormsByEventId(eventId);
                if (userForms==null) { return BadRequest("Error in BL"); }
                return Ok(userForms);
            }
            catch
            {
                return BadRequest("Error in controller");

            }
        }




    }
}
