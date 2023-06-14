using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.DLL.Contracts;
using db.Models;

namespace BookMyEvent.BLL.Services
{
    public class OrganiserFormServices : IOrganiserFormServices
    {
        private readonly IFormRepository _organiserFormRepository;
        private readonly IRegistrationFormFieldRepository _organiserFormFieldsRepository;
        private readonly IFieldTypeRepository _fieldTypeRepository;

        public OrganiserFormServices(IFormRepository organiserFormRepository, IRegistrationFormFieldRepository organiserFormFieldsRepository, IFieldTypeRepository fieldTypeRepository)
        {
            _organiserFormRepository = organiserFormRepository;
            _organiserFormFieldsRepository = organiserFormFieldsRepository;
            _fieldTypeRepository = fieldTypeRepository;
        }


        public async Task<(Guid FormId, string Message)> AddForm(BLForm form, List<BLRegistrationFormFields> registrationFormFields)
        {
            try
            {
                if (form.FormId == Guid.Empty)
                {
                    var mapper = Automapper.InitializeAutomapper();
                    var newForm = await _organiserFormRepository.Add(mapper.Map<Form>(form));
                    foreach (var item in registrationFormFields)
                    {
                        item.FormId = newForm.FormId;
                    }
                    var newRegistrationFormFields = mapper.Map<List<RegistrationFormField>>(registrationFormFields);
                    var result = await _organiserFormFieldsRepository.AddMany(newRegistrationFormFields);
                    return (newForm.FormId, "Added Successfully");
                }
                return (form.FormId, "Form already exists, so nothing to add");
            }
            catch (Exception ex)
            {
                return (Guid.Empty, ex.Message);
            }
        }

        public async Task<(bool IsFormDeleted, string Message)> DeleteForm(Guid id)
        {
            try
            {
                return await _organiserFormRepository.UpdateIsActive(id);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<(BLForm form, List<BLRegistrationFormFields> formFields)>> GetAllFormsCreatedBy(Guid id)
        {
            try
            {
                var forms = await _organiserFormRepository.GetByCreatorId(id);
                var mapper = Automapper.InitializeAutomapper();
                var result = new List<(BLForm form, List<BLRegistrationFormFields> formFields)>();
                foreach (var item in forms)
                {
                    var formFields = await _organiserFormFieldsRepository.GetAllFields(item.FormId);
                    result.Add((mapper.Map<BLForm>(item), mapper.Map<List<BLRegistrationFormFields>>(formFields)));
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<(BLForm form, List<BLRegistrationFormFields> formFields)>();
            }
        }

        public async Task<List<(BLForm form, List<BLRegistrationFormFields> formFields)>> GetAllOrganisationForms(Guid id)
        {
            try
            {
                var forms = await _organiserFormRepository.GetByOrganisationId(id);
                var mapper = Automapper.InitializeAutomapper();
                var result = new List<(BLForm form, List<BLRegistrationFormFields> formFields)>();
                foreach (var item in forms)
                {
                    var formFields = await _organiserFormFieldsRepository.GetAllFields(item.FormId);
                    result.Add((mapper.Map<BLForm>(item), mapper.Map<List<BLRegistrationFormFields>>(formFields)));
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<(BLForm form, List<BLRegistrationFormFields> formFields)>();
            }
        }

        public async Task<List<BLFieldType>> GetFieldTypes()
        {
            try
            {
                var fieldTypes = _fieldTypeRepository.GetAllFieldTypes();
                var mapper = Automapper.InitializeAutomapper();
                return mapper.Map<List<BLFieldType>>(fieldTypes);
            }
            catch (Exception ex)
            {
                return new List<BLFieldType>();
            }
        }

        public async Task<BLForm> GetFormById(Guid id)
        {
            try
            {
                var form = await _organiserFormRepository.Get(id);
                var mapper = Automapper.InitializeAutomapper();
                return mapper.Map<BLForm>(form);
            }
            catch (Exception ex)
            {
                return new BLForm();
            }
        }

        public async Task<List<BLRegistrationFormFields>> GetFormFieldsByFormId(Guid id)
        {
            try
            {
                var formFields = await _organiserFormFieldsRepository.GetAllFields(id);
                var mapper = Automapper.InitializeAutomapper();
                return mapper.Map<List<BLRegistrationFormFields>>(formFields);
            }
            catch (Exception ex)
            {
                return new List<BLRegistrationFormFields>();
            }
        }
    }
}
