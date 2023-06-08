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

        public Task<UserInputForm> Get(Guid inputFormId)
        {
            throw new NotImplementedException();
        }

        public Task<UserInputForm> Update(UserInputForm inputForm)
        {
            throw new NotImplementedException();
        }
    }
}
