using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.BLL.Services;
using BookMyEvent.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner,Secondary_Owner,Peer")]
    public class OrganiserFormController : ControllerBase
    {
        private readonly IOrganiserFormServices _organiserFormServices;
        private readonly FileLogger _fileLogger;
        public OrganiserFormController(IOrganiserFormServices organiserFormServices, FileLogger fileLogger)
        {
            _organiserFormServices = organiserFormServices;
            _fileLogger = fileLogger;
        }
        /// <summary>
        /// Service to Add New Form
        /// </summary>
        /// <param name="form"></param>
        /// <returns>retuns FormId and also ConfirmationMessage</returns>
        [HttpPost("AddForm")]
        public async Task<IActionResult> AddNewForm(BLForm form)
        {
            try
            {
                (Guid FormId, string Message) result = await _organiserFormServices.AddForm(form);
                _fileLogger.AddInfoToFile("[AddNewForm] Adding New Form Success");
                return Ok(result.FormId);
            }
            catch
            {
                _fileLogger.AddExceptionToFile("[AddNewForm] Adding New Form Failure");
                return BadRequest("error");
            }
        }
        /// <summary>
        /// Service to Add Reggistration Form Fields
        /// </summary>
        /// <param name="RegistrationFormFields"></param>
        /// <returns>true if Added else false</returns>
        [HttpPost("AddFormFields")]
        public async Task<IActionResult> AddRegistrationFormFields(List<BLRegistrationFormFields> RegistrationFormFields)
        {
            try
            {
                _fileLogger.AddInfoToFile("[AddRegistrationFormFields] Adding Registration form Fieds Success");
                return Ok(await _organiserFormServices.AddRegistrationFormFields(RegistrationFormFields));
            }
            catch
            {
                _fileLogger.AddExceptionToFile("[AddRegistrationFormFields] Adding RegistrationFormFields Failure");
                return BadRequest(false);
            }
        }
        /// <summary>
        /// Service to verify FormName
        /// </summary>
        /// <param name="formname"></param>
        /// <returns>true if Valid else False</returns>
        [HttpGet("isFormNameTaken/{formname}")]
        public async Task<IActionResult> IsFormNameTaken(string formname)
        {
            _fileLogger.AddInfoToFile("[IsFromTaken] Verifying Form Name Success");
            return Ok(await _organiserFormServices.IsformNameTaken(formname));
        }
        /// <summary>
        /// Service to Get FormById 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns>Returns All the Form Details</returns>
        [HttpGet("FormId")]
        public async Task<IActionResult> GetFormById(Guid FormId)
        {
            try
            {
                if (FormId != null)
                {
                    _fileLogger.AddInfoToFile("[GetFormById] Getting Form By Id Success");
                    return Ok(await _organiserFormServices.GetFormById(FormId));
                }
                _fileLogger.AddInfoToFile("[GetFormByID] Getting FormById NotFound");
                return NotFound();
            }
            catch (Exception ex)
            {
                _fileLogger.AddInfoToFile("[GetFormById] GettingFormById Exception");
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///Service to GetAllFieldTypes
        /// </summary>
        /// <returns>List of fieldTypes</returns>
        [AllowAnonymous]
        [HttpGet("FieldTypes")]
        public async Task<IActionResult> GetAllFieldTypes()
        {
            try
            {
                List<BLFieldType> fieldtypes = await _organiserFormServices.GetFieldTypes();
                if (fieldtypes.Count != 0)
                {
                    _fileLogger.AddInfoToFile("[GetAllFieldTypes] Getting All fieldtypes Success");
                    return Ok(fieldtypes);
                }
                _fileLogger.AddExceptionToFile("[GetAllFieldTypes] Getting All fieldtypes BadRequest");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetAllFieldTypes] Getting All fieldtypes BadRequest");
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Get fieldtypes by form Id 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns>List of FormField</returns>
        [AllowAnonymous]
        [HttpGet("FieldTypesByFormId")]
        public async Task<IActionResult> GetFieldTypesByFormId(Guid FormId)
        {
            try
            {
                List<BLRegistrationFormFields> formfields = await _organiserFormServices.GetFormFieldsByFormId(FormId);
                if (formfields.Count != 0)
                {
                    _fileLogger.AddInfoToFile("[GetFieldTypesByFormId] Getting All fieldtypes byformid Success");
                    return Ok(formfields);
                }
                _fileLogger.AddExceptionToFile("[GetFieldTypesBbyFormId] Getting All fieldtypesbyformid Notfound");
                return NotFound();
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetFieldTypesByFormId] Getting All fieldtypes By formId Badrequest");
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to getAll Organization Forms
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns>List of Forms associated with the Organization</returns>
        [HttpGet("OrganizationForms")]
        public async Task<IActionResult> GetAllOrganizationForms(Guid OrgId)
        {
            try
            {
                var result = await _organiserFormServices.GetAllOrganisationForms(OrgId);
                _fileLogger.AddInfoToFile("[GetAllOrganizationForms] Getting All OranizationForms Success");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetAllOrganizationForms] Getting All OrganizationForms Exception");
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Service to Get All Forms By an Organizer
        /// </summary>
        /// <param name="OrganizerId"></param>
        /// <returns>List of Forms </returns>
        [HttpGet("OrganizerForms")]
        public async Task<IActionResult> GetAllFormsCreatedByOrganizer(Guid OrganizerId)
        {
            try
            {
                var result = await _organiserFormServices.GetAllFormsCreatedBy(OrganizerId);
                if (result.Count != 0)
                {
                    _fileLogger.AddInfoToFile("[GetAllFormsCreatedByOrganizer] Getting All ORganizerForms Success");
                    return Ok(result);
                }
                _fileLogger.AddExceptionToFile("[GetAllFormsCreatedByOrganizer] Getting All OrganizerForms NorFound");
                return NotFound();
            }
            catch (Exception e)
            {
                _fileLogger.AddExceptionToFile("[GetAllFormsCreatedByOrganizer] Getting All OrganizerForms Exception");
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Service to Delete Form 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns>true if successful else false along with a message</returns>
        [HttpDelete("DeleteForm")]
        public async Task<IActionResult> DeleteForm(Guid FormId)
        {
            try
            {
                var result = await _organiserFormServices.DeleteForm(FormId);
                if (result.IsFormDeleted == true)
                {
                    _fileLogger.AddInfoToFile("[DeleteForm] DeletingForm Success");
                    return Ok(result);
                }
                _fileLogger.AddExceptionToFile("[DeleteForm] DeleteForm Failure");
                return NotFound();
            }
            catch (Exception ex)
            {
                _fileLogger.AddExceptionToFile("[GetAllFieldTypes] Delete Form Exception");
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet("GetFileTypes")]
        public async Task<IActionResult> GetFileTypes()
        {
            try
            {
                return Ok(await _organiserFormServices.GetFileTypes());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
