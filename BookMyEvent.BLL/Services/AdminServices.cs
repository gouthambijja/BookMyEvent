using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using BookMyEvent.WebApi.Utilities;

namespace BookMyEvent.BLL.Services
{
    public class AdminServices : IAdminService
    {
        private readonly Mapper mapper;
        private readonly IAdministrationRepository _administrationRepository;
        private readonly IAccountCredentialsRepository _accountCredentialsRepository;
        public AdminServices(IAdministrationRepository administrationRepository, IAccountCredentialsRepository accountCredentialsRepository)
        {
            _administrationRepository = administrationRepository;
            _accountCredentialsRepository = accountCredentialsRepository;
            mapper = Automapper.InitializeAutomapper();
        }
        public async Task<BLAdministrator> CreateAdministrator(BLAdministrator secondaryAdmin)
        {
            try
            {
                if (secondaryAdmin is not null)
                {
                    Console.WriteLine(secondaryAdmin.AdministratorName);
                    var acccred = await _accountCredentialsRepository.AddCredential(new AccountCredential { Password = secondaryAdmin.Password, UpdatedOn = DateTime.Now });
                    secondaryAdmin.AccountCredentialsId = acccred.AccountCredentialsId;
                    Administration administration = mapper.Map<Administration>(secondaryAdmin);
                    Console.WriteLine(administration.RoleId);
                    Console.WriteLine(administration.AccountCredentialsId);
                    return mapper.Map<BLAdministrator>(await _administrationRepository.AddAdministrator(administration));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return new BLAdministrator();
            }
        }

        public async Task<bool> BlockAdmin(Guid AdminId, Guid blockedBy)
        {
            if (AdminId != null)
            {
                return await _administrationRepository.UpdateDeletedByAndIsActive(blockedBy, AdminId);
            }
            return false;
        }

        public async Task<bool> ChangeAdminPassword(Guid AdminId, string Password)
        {
            try
            {
                if (AdminId != null)
                {
                    return await _administrationRepository.ChangeAdministratorPassword(AdminId, Password);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<BLAdministrator> GetAdminById(Guid AdminId)
        {
            try
            {
                if (AdminId != null)
                {
                    Administration Admin = await _administrationRepository.GetAdministratorById(AdminId);
                    return mapper.Map<Administration, BLAdministrator>(Admin);
                }
                return new BLAdministrator();
            }
            catch (Exception ex)
            {
                return new BLAdministrator();
            }
        }
        public async Task<List<BLAdministrator>> GetAllSecondaryAdmins()
        {
            try
            {
                List<Administration> ListOfAdmins = await _administrationRepository.GetSecondaryAdministrators();
                Console.WriteLine(ListOfAdmins.Count);
                return mapper.Map<List<Administration>, List<BLAdministrator>>(ListOfAdmins);
            }
            catch (Exception ex)
            {
                return new List<BLAdministrator>();
            }
        }
        public async Task<bool> DeleteAdmin(Guid Deletedby, Guid SecondaryAdminId)
        {
            try
            {
                if (SecondaryAdminId != null)
                {
                    return await _administrationRepository.UpdateDeletedByAndIsActive(Deletedby, SecondaryAdminId);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<BLAdministrator> UpdateAdministrator(BLAdministrator secondaryAdmin)
        {
            try
            {

                if (secondaryAdmin is not null)
                {
                    Administration Admin = await _administrationRepository.UpdateAdministrator(mapper.Map<BLAdministrator, Administration>(secondaryAdmin));
                    return secondaryAdmin;
                }
                return new BLAdministrator();
            }
            catch (Exception ex)
            {
                return new BLAdministrator();
            }
        }
        public async Task<BLAdministrator> LoginAdmin(string email, string password, string role)
        {
            try
            {
                Mapper mapper = Automapper.InitializeAutomapper();
                Console.WriteLine(role);
                Console.WriteLine(" ------ " + Roles.Admin.ToString());
                if (role == Roles.Admin.ToString())
                {
                    Administration? Admin = await _administrationRepository.GetAdministratorByEmail(email);
                    if (Admin == null) { return null; }
                    if (await _accountCredentialsRepository.IsValidCredential(Admin.AccountCredentialsId, password))
                    {
                        Console.WriteLine(Admin.PhoneNumber);
                        return mapper.Map<BLAdministrator>(Admin);
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLAdministrator>> AdminsCreatedByAdmin(Guid AdminId)
        {
            try
            {
                if (AdminId != null)
                {
                    List<Administration> CreatedAdmins = await _administrationRepository.GetCreatedAdministratorsById(AdminId);
                    return mapper.Map<List<Administration>, List<BLAdministrator>>(CreatedAdmins);
                }
                return new List<BLAdministrator>();
            }
            catch (Exception ex)
            {
                return new List<BLAdministrator>();
            }
        }
    }
}
