using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.BLL.RequestModels;
using BookMyEvent.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyEvent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserFormController : ControllerBase
    {
        private readonly IOrganiserFormServices _organiserFormServices;
        public OrganiserFormController(IOrganiserFormServices organiserFormServices)
        {
            _organiserFormServices = organiserFormServices;
        }
        [HttpPost("AddForm")]
        public async Task<IActionResult> AddNewForm(BLForm form)
        {
            try
            {
                (Guid FormId, string Message) result = await _organiserFormServices.AddForm(form);
                return Ok(result.FormId);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("AddFormFields")]
        public async Task<IActionResult> AddRegistrationFormFields(List<BLRegistrationFormFields> RegistrationFormFields)
        {
            try
            {
                return Ok(await _organiserFormServices.AddRegistrationFormFields(RegistrationFormFields));
            }
            catch
            {
                return BadRequest(false);
            }
        }
        [HttpGet("isFormNameTaken/{formname}")]
        public async Task<IActionResult> IsFormNameTaken(string formname)
        {
            return Ok(await _organiserFormServices.IsformNameTaken(formname));
        }
        [HttpGet("FormId")]
        public async Task<IActionResult> GetFormById(Guid FormId)
        {
            try
            {
                if (FormId != null)
                {
                    return Ok(await _organiserFormServices.GetFormById(FormId));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("FieldTypes")]
        public async Task<IActionResult> GetAllFieldTypes()
        {
            try
            {
                List<BLFieldType> fieldtypes = await _organiserFormServices.GetFieldTypes();
                if (fieldtypes.Count != 0)
                {
                    return Ok(fieldtypes);  
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("FieldTypesByFormId")]
        public async Task<IActionResult> GetFieldTypesByFormId(Guid FormId)
        {
            try
            {
                List<BLRegistrationFormFields> formfields = await _organiserFormServices.GetFormFieldsByFormId(FormId);
                if (formfields.Count != 0)
                {
                    return Ok(formfields);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("OrganizationForms")]
        public async Task<IActionResult> GetAllOrganizationForms(Guid OrgId)
        {
            try
            {
                var result = await _organiserFormServices.GetAllOrganisationForms(OrgId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("OrganizerForms")]
        public async Task<IActionResult> GetAllFormsCreatedByOrganizer(Guid OrganizerId)
        {
            try
            {
                var result = await _organiserFormServices.GetAllFormsCreatedBy(OrganizerId);
                if (result.Count != 0)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("DeleteForm")]
        public async Task<IActionResult> DeleteForm(Guid FormId)
        {
            try
            {
                var result = await _organiserFormServices.DeleteForm(FormId);
                if (result.IsFormDeleted == true)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
