using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    //It is Used for Performing Operations on Credentials//
    public interface IAccountCredentialsRepository
    {
        /// <summary>
        /// Method to add Credentials of a particular user by taking credential as input 
        /// </summary>
        /// <param name="credential"></param>
        /// <returns>AccountCredential Object</returns>
        public Task<AccountCredential> AddCredential(AccountCredential credential);
        /// <summary>
        /// Method to update the Credentials of a Particular User by taking Credential object as input
        /// </summary>
        /// <param name="credential"></param>
        /// <returns>AccountCredential Object</returns>
        public Task<AccountCredential> UpdateCredential(AccountCredential credential);
        /// <summary>
        /// Method to Get Credentials of a particular User by taking Id as input
        /// </summary>
        /// <param name="AccountCredentialId"></param>
        /// <returns>AccountCredential Object</returns>
        public Task<AccountCredential> GetCredential(Guid AccountCredentialId);
        /// <summary>
        /// This method is compares the given password with password present in the table 
        /// </summary>
        /// <param name="AccountCredentialId"></param>
        /// <returns>return true if same passwords else false</returns>
        public Task<bool> IsValidCredential(Guid? AccountCredentialId,string password);
    }
}
