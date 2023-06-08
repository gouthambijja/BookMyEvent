using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class UserInputFormFieldsRepository : IUserInputFormFieldsRepository
    {
        private readonly EventManagementSystemTeamZealContext _DBContext;
        public UserInputFormFieldsRepository(EventManagementSystemTeamZealContext dbContext)
        {
            _DBContext = dbContext;
        }
        public async Task<UserInputFormField> Add(UserInputFormField formField)
        {
            try
            {
                await _DBContext.UserInputFormFields.AddAsync(formField);
                await _DBContext.SaveChangesAsync();
                await _DBContext.Entry(formField).GetDatabaseValuesAsync();
                return formField;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddMany(List<UserInputFormField> formFields)
        {
            try
            {
                await _DBContext.UserInputFormFields.AddRangeAsync(formFields);
                await _DBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Delete(Guid UserInputFormFieldId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserInputFormField>> GetAllFieldsByFormId(Guid UserInputFormId)
        {
            try
            {
                var userInputFormFields = await _DBContext.UserInputFormFields.Where(e => e.UserInputFormId == UserInputFormId).ToListAsync();
                return userInputFormFields;
            }
            catch
            {
                return null;
            }
        }

        public Task<UserInputFormField> Update(UserInputFormField formField)
        {
            throw new NotImplementedException();
        }
    }
}
