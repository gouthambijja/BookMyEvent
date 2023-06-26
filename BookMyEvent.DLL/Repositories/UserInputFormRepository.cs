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
    public class UserInputFormRepository : IUserInputFormRepository
    {
        private readonly EventManagementSystemTeamZealContext _DBContext;
        public UserInputFormRepository(EventManagementSystemTeamZealContext dBContext)
        {
            _DBContext = dBContext;
        }

        public async Task<UserInputForm> Add(UserInputForm inputForm)
        {
            try
            {
                inputForm.UserInputFormId = Guid.NewGuid();
                _DBContext.UserInputForms.Add(inputForm);
                await _DBContext.SaveChangesAsync();
                await _DBContext.Entry(inputForm).GetDatabaseValuesAsync();
                return inputForm;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Delete(Guid inputFormId)
        {
            try
            {
                var _UserInputForm = await _DBContext.UserInputForms.
                     Where(e => e.UserInputFormId == inputFormId).FirstAsync();
                if (_UserInputForm == null) return false;
                _DBContext.Remove(_UserInputForm);
                await _DBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Guid>?> GetInputFormIdByUserIdAndEventId(Guid userId, Guid eventId)
        {
            try
            {
                List<Guid> inputFormIds = await _DBContext.UserInputForms.Where(f => f.UserId == userId && f.EventId == eventId)
                     .Select(f => f.UserInputFormId)
                     .ToListAsync();
                if (inputFormIds != null)
                {

                    return inputFormIds;
                }
                else { return null; }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<UserInputForm>?> GetUserInputFormsByEventId( Guid eventId)
        {
            try
            {
                List<UserInputForm> inputForms = await _DBContext.UserInputForms.Where(f => f.EventId == eventId).ToListAsync();
                if (inputForms != null)
                {

                    return inputForms;
                }
                else { return null; }
            }
            catch
            {
                return null;
            }
        }

        public Task<UserInputForm> Update(UserInputForm inputForm)
        {
            throw new NotImplementedException();
        }
    }
}
