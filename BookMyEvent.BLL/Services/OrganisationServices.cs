using AutoMapper;
using BookMyEvent.BLL.Utilities;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Services
{
    public class OrganisationServices : IOrganisationServices
    {

        private readonly IOrganisationRepository _organisationRepository;
        private readonly IAdministrationRepository _administrationRepository;
        private readonly Mapper mapper;

        public OrganisationServices(IOrganisationRepository organisationRepository, IAdministrationRepository administrationRepository)
        {
            _organisationRepository = organisationRepository;
            _administrationRepository = administrationRepository;
            mapper = Automapper.InitializeAutomapper();
        }

        public async Task<(BLOrganisation? org, bool IsSuccessfull, string Message)> CreateOrganisation(BLOrganisation organisation)
        {
            try
            {
                if (!(await (_organisationRepository.IsOrgNameAvailable(organisation.OrganisationName))))
                {
                    var result = await _organisationRepository.AddOrganisation(mapper.Map<Organisation>(organisation));
                    return (mapper.Map<BLOrganisation>(result.org), result.IsSuccessfull, result.Message);
                }
                return (null, false, "Organisation name already exists");
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }

        public async Task<(bool IsOrganisationBlockToggled, string Message)> BlockOrganisation(Guid organisationId, Guid blockerId)
        {
            try
            {
                var result = await _organisationRepository.ChangeIsActiveToFalse(organisationId);
                if (result)
                {
                    var res2 = await _administrationRepository.UpdateAllOrganisationOrganisersIsActive(organisationId, blockerId);
                    if (res2)
                    {
                        return (true, "Organisation blocked successfully");
                    }
                    return (false, "Organisation blocked but organiser block unsuccessfull");
                }
                return (false, "Organisation blocked/unblock unsuccessfull");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(List<BLOrganisation> bLOrganisations, int totalBLOrganisations)> GetAllOrganisations(int pageNumber, int pageSize)
        {

            try
            {
                var result = await _organisationRepository.GetAllOrganisation(pageNumber, pageSize);
                var mappedResult = mapper.Map<List<BLOrganisation>>(result.organisations);
                return new(mappedResult, result.totalOranisations);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BLL: " + ex.Message);
            }
        }

        public async Task<BLOrganisation> GetOrganisationById(Guid organisationId)
        {
            try
            {
                var result = await _organisationRepository.GetOrganisationById(organisationId);
                return mapper.Map<BLOrganisation>(result);
            }
            catch (Exception ex)
            {
                return new BLOrganisation();
            }
        }

        public async Task<Guid?> GetOrgIdByName(string name)
        {
            try
            {
                Guid? orgId = await _organisationRepository.GetOrgIdByName(name);
                return orgId;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> IsOrganisationNameTaken(string orgName)
        {
            try
            {
                return await _organisationRepository.IsOrgNameAvailable(orgName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BLOrganisation> UpdateOrganisation(BLOrganisation organisation)
        {
            try
            {
                var result = await _organisationRepository.UpdateOrganisation(mapper.Map<Organisation>(organisation));
                return mapper.Map<BLOrganisation>(result);
            }
            catch (Exception ex)
            {
                return new BLOrganisation();
            }
        }
        public async Task<bool> AcceptOrganisation(Guid orgId)
        {
            try
            {
                return await _organisationRepository.ChangeIsActiveToTrue(orgId);
            }
            catch
            {
                return false;
            }
        }
    }
}
