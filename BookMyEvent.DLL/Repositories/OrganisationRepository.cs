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
    public class OrganisationRepository:IOrganisationRepository
    {
        private readonly EventManagementSystemTeamZealContext _dbcontext;
        public OrganisationRepository(EventManagementSystemTeamZealContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<(Organisation? org, bool IsSuccessfull, string Message)> AddOrganisation(Organisation organisation)
        {
            try
            {
                _dbcontext.Organisations.Add(organisation);
                await _dbcontext.SaveChangesAsync();
                await _dbcontext.Entry(organisation).GetDatabaseValuesAsync();
                return (organisation, true, "Organisation created successfully");
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }


        public async Task<Organisation?> GetOrganisationById(Guid orgId)
        {
            try
            {
                Organisation? organisation = await _dbcontext.Organisations.FindAsync(orgId);
                if (organisation != null)
                {
                    return organisation;
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


        public async Task<(List<Organisation>? organisations, int totalOranisations)> GetAllOrganisation(int pageNumber, int pageSize)
        {
            try
            {
                var organisations = await _dbcontext.Organisations
                    .Where(o => o.IsActive == true)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalCount = await _dbcontext.Organisations
                    .Where(o => o.IsActive == true)
                    .CountAsync();

                return new ( organisations,totalCount );
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DLL: " + ex.Message);
            }
        }
        public async Task<Organisation?> UpdateOrganisation(Organisation updatedOrganisation)
        {
            try
            {
                Organisation? organisation = await _dbcontext.Organisations.FindAsync(updatedOrganisation.OrganisationId);

                if (organisation != null)
                {
                    organisation.OrganisationName = updatedOrganisation.OrganisationName;
                    organisation.OrganisationDescription = updatedOrganisation.OrganisationDescription;
                    organisation.Location = updatedOrganisation.Location;
                    organisation.UpdatedOn = DateTime.Now;
                    await _dbcontext.SaveChangesAsync();
                    await _dbcontext.Entry(organisation).GetDatabaseValuesAsync();
                    return organisation;
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


        public async Task<bool> DeleteOrganisationById(Guid orgId)
        {
            try
            {
                Organisation? organisation = await _dbcontext.Organisations.FindAsync(orgId);
                if (organisation != null)
                {
                    organisation.IsActive = false;
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

        public async Task<bool> IsOrgNameAvailable(string orgName)
        {
            try
            {
                Organisation? organisation = await _dbcontext.Organisations.Where(o => o.OrganisationName == orgName).FirstOrDefaultAsync();
                if (organisation is null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ToggleIsActive(Guid orgId)
        {
            try
            {
                Organisation? organisation = await _dbcontext.Organisations.FindAsync(orgId);
                if (organisation != null)
                {
                    organisation.IsActive = !organisation.IsActive;
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
