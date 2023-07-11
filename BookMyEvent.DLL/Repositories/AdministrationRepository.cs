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
                    await _dbcontext.Administrations.AddAsync(administrator);

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
        public async Task<Administration?> GetAdminByEmail(string email)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.Where(a => a.Email == email && a.RoleId == 1).FirstOrDefaultAsync();
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
        public async Task<List<Administration>> GetSecondaryAdministrators()
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.RoleId == 1 && a.IsActive == true).ToListAsync();
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
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.RoleId == 2 && a.IsActive == true && a.AcceptedBy==null && a.RejectedBy==null).ToListAsync();
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
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.OrganisationId == OrgId && a.RoleId == 4 && a.IsActive == true && a.AcceptedBy == null && a.RejectedBy == null).ToListAsync();
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


        public async Task<bool> UpdateIsAcceptedAndAcceptedBy(Guid? acceptedByUserId, Guid acceptedAccountId)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(acceptedAccountId);
                if (administrator != null)
                {
                    administrator.AcceptedBy = acceptedByUserId;
                    administrator.IsAccepted = true;
                    administrator.UpdatedOn=DateTime.Now;
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


        public async Task<bool> UpdateRejectedByAndIsActive(Guid rejectedAccountId, Guid? rejectedByUserId, string reason)
        {
            try
            {
                Administration? administrator = await _dbcontext.Administrations.FindAsync(rejectedAccountId);
                if (administrator != null)
                {

                    administrator.RejectedBy = rejectedByUserId;
                    administrator.RejectedReason = reason;
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

                    administrator.DeletedBy = deletedByUserId;
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
        public async Task<bool> ChangeAdministratorPassword(Guid AdministratorID, string Password)
        {
            try
            {
                var Admin = await _dbcontext.Administrations.FindAsync(AdministratorID);
                if (Admin != null)
                {
                    var password = await _dbcontext.AccountCredentials.FindAsync(Admin.AccountCredentialsId);
                    Console.WriteLine(password.Password);
                    if (password != null)
                    {
                        password.Password = Password;
                        await _dbcontext.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAllOrganisationOrganisersIsActive(Guid orgId, Guid updatedBy)
        {
            try
            {
                Organisation? organisation = await _dbcontext.Organisations.FindAsync(orgId);
                if (organisation != null)
                {
                    organisation.IsActive = false;
                }
                else
                {
                    return false;
                }
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.OrganisationId == orgId && a.IsActive == true).ToListAsync();
                if (administrators != null)
                {
                    foreach (var administrator in administrators)
                    {
                        administrator.IsActive = false;
                        administrator.DeletedBy = updatedBy;
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

        public async Task<List<Administration>> GetAdministrationsByOrgId(Guid OrgId)
        {
            try
            {
                List<Administration> administrators = await _dbcontext.Administrations.Where(a => a.OrganisationId == OrgId && a.IsActive == true && a.IsAccepted==true).ToListAsync();
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

        public async Task<bool> IsEmailExists(string Email)
        {
            try
            {
                var administrator = await _dbcontext.Administrations.Where(a => a.Email == Email).FirstOrDefaultAsync();
                if (administrator != null)
                {
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
