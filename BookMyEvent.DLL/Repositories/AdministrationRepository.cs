using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyEvent.DLL.Repositories
{
    public class AdministrationRepository : IAdministrationRepository
    {
        private readonly EventManagementSystemTeamZealContext _dbcontext;
        public AdministrationRepository(EventManagementSystemTeamZealContext dbcontext) 
        {
            _dbcontext = dbcontext; 
        }

        public async Task<Administration?> AddAdministrator(Administration administrator)
        {
            try
            {
                if (administrator != null)
                {
                    _dbcontext.Administrations.Add(administrator);
                    await _dbcontext.SaveChangesAsync();
                    await _dbcontext.Entry(administrator).GetDatabaseValuesAsync();
                    return administrator;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<Administration?> GetAdministratorById(Guid Id)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(Id);
                if (administrator != null)
                {
                    return administrator;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

       
        public async Task<Administration?> GetAdministratorByEmail(string email)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.Where(a => a.Email == email).FirstOrDefaultAsync();
                if (administrator != null)
                {
                    return administrator;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<Administration?> GetAdministratorByGoogleId(string GoogleId)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.Where(a => a.GoogleId == GoogleId).FirstOrDefaultAsync();
                if (administrator != null)
                {
                    return administrator;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetAdministrators()
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.RoleId != 1 && a.IsActive == true && a.IsAccepted == true).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetAcceptedAdministratorsById(Guid Id)
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.AcceptedBy == Id && a.IsActive == true).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetCreatedAdministratorsById(Guid Id)
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.CreatedBy == Id && a.IsActive == true).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetPrimaryAdministrators()
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.RoleId == 2 && a.IsActive == true && a.IsAccepted == true).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetSecondaryAdministratorsByOrgId(Guid OrgId)
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.OrganisationId == OrgId && a.RoleId == 3 && a.IsActive == true && a.IsAccepted == true).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetPeerAdministratorsByOrgId(Guid OrgId)
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.OrganisationId == OrgId && a.RoleId == 4 && a.IsActive == true && a.IsAccepted == true).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetPrimaryAdministratorRequests()
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.RoleId == 2 && a.IsActive == true && a.IsAccepted == false).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Administration>?> GetPeerAdministratorRequests(Guid OrgId)
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.OrganisationId == OrgId && a.RoleId == 4 && a.IsActive == true && a.IsAccepted == false).ToListAsync();
                if (administrators != null)
                {
                    return administrators;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<Administration?> UpdateAdministrator(Administration updatedAdministrator)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(updatedAdministrator.AdministratorId);
                if (administrator != null)
                {

                    administrator.AdministratorName = updatedAdministrator.AdministratorName;
                    administrator.AdministratorAddress = updatedAdministrator.AdministratorAddress;
                    administrator.Email = updatedAdministrator.Email;
                    administrator.PhoneNumber = updatedAdministrator.PhoneNumber;
                    administrator.UpdatedOn = DateTime.Now;
                    await _dbcontext.SaveChangesAsync();
                    await _dbcontext.Entry(administrator).GetDatabaseValuesAsync();
                    return administrator;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> DeleteAdministratorById(Guid Id)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(Id);
                if (administrator != null)
                {
                    _dbcontext.Administrations.Remove(administrator);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> ToggleIsActive(Guid Id)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(Id);
                if (administrator != null)
                {
                    administrator.IsActive = !administrator.IsActive;
                    await _dbcontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> UpdateIsAcceptedAndAcceptedBy(Guid acceptedByUserId, Guid acceptedAccountId)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(acceptedAccountId);
                if (administrator != null)
                {

                    administrator.AcceptedBy = acceptedByUserId;
                    administrator.IsAccepted = true;
                    await _dbcontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> UpdateRejectedByAndIsActive(Guid rejectedByUserId, Guid rejectedAccountId)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(rejectedAccountId);
                if (administrator != null)
                {

                    administrator.AcceptedBy = rejectedByUserId;
                    administrator.IsActive = false;
                    await _dbcontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> UpdateDeletedByAndIsActive(Guid deletedByUserId, Guid deletedAccountId)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(deletedAccountId);
                if (administrator != null)
                {

                    administrator.AcceptedBy = deletedByUserId;
                    administrator.IsActive = false;
                    await _dbcontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAdministratorsByOrgId(Guid deletedByUserId, Guid OrgId)
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.OrganisationId == OrgId && a.IsActive == true).ToListAsync();
                if (administrators != null)
                {
                    foreach (var administrator in administrators)
                    {
                        administrator.IsActive = false;
                    }
                    await _dbcontext.SaveChangesAsync();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
