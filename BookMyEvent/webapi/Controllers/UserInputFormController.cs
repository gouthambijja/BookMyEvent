using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInputFormController:ControllerBase
    {
        private readonly IUserInputFormService _userInputFormService;
        public UserInputFormController(IUserInputFormService userInputFormService)
        {
            _userInputFormService = userInputFormService;
        }

        [HttpPost("submitform")]
        public async Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>?> SubmitUserInputForm([FromBody] List<(BLUserInputForm userInputForm, List<BLUserInputFormField> userInputFormFields)> userForms)
        {
            try
            {
                return await _userInputFormService.SubmitUserInputForm(userForms);
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("GetUserFormsOfUserIdByEventId")]

        public async Task<List<List<BLUserInputFormField>>?> GetUserFormsOfUserIdByEventId(Guid userId, Guid EventId)
        {
            try
            {
                return await _userInputFormService.GetUserFormsOfUserIdByEventId(userId, EventId);
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("GetAllUserFormsByEventId")]
        public async Task<List<(BLUserInputForm userInputForm, List<BLUserInputFormField> UserInputFormFields)>?> GetAllUserFormsByEventId(Guid eventId)
        {
            try
            {
                return await _userInputFormService.GetAllUserFormsByEventId(eventId);
            }
            catch
            {
                return null;
            }
        }




    }
}
