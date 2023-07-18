using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.DLL.Contracts;
using db.Models;
using AutoMapper;
using BookMyEvent.BLL.Utilities;


namespace BookMyEvent.BLL.Services
{
    public class OrganiserFormServices : IOrganiserFormServices
    {
        private readonly IFormRepository _organiserFormRepository;
        private readonly IRegistrationFormFieldRepository _organiserFormFieldsRepository;
        private readonly IFieldTypeRepository _fieldTypeRepository;
        private readonly Mapper mapper;

        public OrganiserFormServices(IFormRepository organiserFormRepository, IRegistrationFormFieldRepository organiserFormFieldsRepository, IFieldTypeRepository fieldTypeRepository)
        {
            _organiserFormRepository = organiserFormRepository;
            _organiserFormFieldsRepository = organiserFormFieldsRepository;
            _fieldTypeRepository = fieldTypeRepository;
            mapper = Automapper.InitializeAutomapper();
        }

        public async Task<(Guid FormId, string Message)> AddForm(BLForm form)
        {
            try
            {
                if (form.FormId == Guid.Empty)
                {
                    var newForm = await _organiserFormRepository.Add(mapper.Map<Form>(form));
                    //foreach (var item in registrationFormFields)
                    //{
                    //    item.FormId = newForm.FormId;
                    //}
                    //var newRegistrationFormFields = mapper.Map<List<RegistrationFormField>>(registrationFormFields);
                    //var result = await _organiserFormFieldsRepository.AddMany(newRegistrationFormFields);
                    return (newForm.FormId, "Added Successfully");
                }
                return (form.FormId, "Form already exists, so nothing to add");
            }
            catch (Exception ex)
            {
                return (Guid.Empty, ex.Message);
            }
        }
        public async Task<bool> AddRegistrationFormFields(List<BLRegistrationFormFields> registrationFormFields)
        {
            try
            {
                //var newRegistrationFormFields = mapper.Map<List<RegistrationFormField>>(registrationFormFields);
                List<RegistrationFormField> newRegistrationFormFields = new List<RegistrationFormField>();
                foreach (var formField in registrationFormFields)
                {
                    newRegistrationFormFields.Add(new RegistrationFormField()
                    {
                        IsRequired = formField.IsRequired,
                        Validations = formField.Validations,
                        Options = formField.Options,
                        FormId = formField.FormId,
                        Lable = formField.Lable,
                        FieldTypeId = formField.FieldTypeId
                    });
                }
                var result = await _organiserFormFieldsRepository.AddMany(newRegistrationFormFields);
                return result;
            }
            catch
            {
                return false;
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
                return null;
            }
        }

        public async Task<List<BLForm>> GetAllOrganisationForms(Guid id)
        {
            try
            {
                var forms = await _organiserFormRepository.GetByOrganisationId(id);
                return mapper.Map<List<BLForm>>(forms);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BLFieldType>> GetFieldTypes()
        {
            try
            {
                var fieldTypes = await _fieldTypeRepository.GetAllFieldTypes();
                return mapper.Map<List<BLFieldType>>(fieldTypes);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BLForm> GetFormById(Guid id)
        {
            try
            {
                var form = await _organiserFormRepository.Get(id);
                return mapper.Map<BLForm>(form);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BLRegistrationFormFields>> GetFormFieldsByFormId(Guid id)
        {
            try
            {
                var formFields = await _organiserFormFieldsRepository.GetAllFields(id);
                return mapper.Map<List<BLRegistrationFormFields>>(formFields);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsformNameTaken(string formName)
        {
            try
            {
                return await _organiserFormRepository.IsformNameTaken(formName);
            }
            catch
            {
                return true;
            }
        }
    }
}
