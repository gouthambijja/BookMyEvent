using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class RegistrationFormFieldRepository : IRegistrationFormFieldRepository
    {
        private readonly EventManagementSystemTeamZealContext _DBContext;
        public RegistrationFormFieldRepository(EventManagementSystemTeamZealContext? dBContext)
        {
            _DBContext = dBContext;
        }

        public async Task<RegistrationFormField?> Add(RegistrationFormField registrationFormField)
        {
            try
            {
                await _DBContext.RegistrationFormFields.AddAsync(registrationFormField);
                await _DBContext.SaveChangesAsync();
                await _DBContext.Entry(registrationFormField).GetDatabaseValuesAsync();
                return registrationFormField;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddMany(List<RegistrationFormField> registrationFormFieldList)
        {
            try
            {
                Console.WriteLine(registrationFormFieldList.Count());
                await _DBContext.RegistrationFormFields.AddRangeAsync(registrationFormFieldList);
                await _DBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return false;

        }

        public async Task<bool> Delete(Guid registrationFormFieldId)
        {
            try
            {
                var _registrationFormField = await _DBContext.RegistrationFormFields.
                     Where(e => e.RegistrationFormFieldId == registrationFormFieldId).FirstOrDefaultAsync();
                _DBContext.Remove(_registrationFormField);
                await _DBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<RegistrationFormField> Get(Guid registrationFormFieldId)
        {
            try
            {
                return await _DBContext.RegistrationFormFields.Where(e => e.RegistrationFormFieldId == registrationFormFieldId).FirstAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<RegistrationFormField>> GetAllFields(Guid FormId)
        {
            try
            {
                return await _DBContext.RegistrationFormFields.Where(e => e.FormId == FormId).ToListAsync();
            }
            catch
            {
                {
                    return null;
                }
            }

        }
        public async Task<RegistrationFormField> Update(RegistrationFormField registrationFormField)
        {
            try
            {
                var _registrationFormField = await _DBContext.RegistrationFormFields.Where(e => e.RegistrationFormFieldId == registrationFormField.RegistrationFormFieldId).FirstAsync();
                _registrationFormField.IsRequired = registrationFormField.IsRequired;
                await _DBContext.SaveChangesAsync();
                return await _DBContext.RegistrationFormFields.Where(e => e.RegistrationFormFieldId == registrationFormField.RegistrationFormFieldId).FirstAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
