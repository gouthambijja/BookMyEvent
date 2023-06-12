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
    public class AccountCredentialRepository : IAccountCredentialsRepository
    {
        EventManagementSystemTeamZealContext context;
        public AccountCredentialRepository(EventManagementSystemTeamZealContext context)
        {
            this.context = context;
        }
        public async Task<AccountCredential> AddCredential(AccountCredential credential)
        {
            try
            {
                if (credential != null)
                {
                    await context.AccountCredentials.AddAsync(credential);
                    await context.SaveChangesAsync();
                    await context.Entry(credential).GetDatabaseValuesAsync();
                    return credential;
                }
                else
                {
                    return new AccountCredential();
                }
            }
            catch (Exception ex)
            {
                return new AccountCredential();
            }
        }
        public async Task<AccountCredential> UpdateCredential(AccountCredential credential)
        {
            try
            {
                var AccountCredential = await context.AccountCredentials.FirstOrDefaultAsync(e => e.AccountCredentialsId.Equals(credential.AccountCredentialsId));
                AccountCredential.Password = credential.Password;
                AccountCredential.UpdatedOn = credential.UpdatedOn;
                await context.SaveChangesAsync();
                await context.Entry(credential).GetDatabaseValuesAsync();
                return credential;
            }
            catch (Exception ex)
            {
                return new AccountCredential();
            }
        }
        public async Task<AccountCredential> GetCredential(Guid AccountCredentialId)
        {
            return await context.AccountCredentials.FirstOrDefaultAsync(e => e.AccountCredentialsId.Equals(AccountCredentialId));
        }

        public async Task<bool> IsValidCredential(Guid? AccountCredentialId, string password)
        {
            try
            {
                var AccountCredential = await context.AccountCredentials.FirstOrDefaultAsync(e => e.AccountCredentialsId == AccountCredentialId && e.Password == password);
                if (AccountCredential == null) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
