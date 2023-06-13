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
        private readonly IAdministrationRepository _administrationRepository;
        private readonly IAccountCredentialsRepository _accountCredentialsRepository;
        public AdminServices(IAdministrationRepository administrationRepository, IAccountCredentialsRepository accountCredentialsRepository)
        {
            _administrationRepository = administrationRepository;
            _accountCredentialsRepository = accountCredentialsRepository;
        }
        public async Task<BLAdministrator> CreateAdministrator(BLAdministrator secondaryAdmin)
        {
            if (secondaryAdmin is not null)
            {
                var mapper = Automapper.InitializeAutomapper();
                Administration administration = mapper.Map<Administration>(secondaryAdmin);
                await _administrationRepository.AddAdministrator(administration);
                return secondaryAdmin;
            }
            return new BLAdministrator();
        }

        public async Task<bool> BlockAdmin(Guid AdminId)
        {
            if (AdminId != null)
            {
                return await _administrationRepository.ToggleIsActive(AdminId);
            }
            return false;
        }

        public async Task<bool> ChangeAdminPassword(Guid AdminId, string Password)
        {
            if (AdminId != null)
            {
                return await _administrationRepository.ChangeAdministratorPassword(AdminId, Password);
            }
            return false;
        }
        public async Task<BLAdministrator> GetAdminById(Guid AdminId)
        {
            if (AdminId != null)
            {
                Administration Admin = await _administrationRepository.GetAdministratorById(AdminId);
                var mapper = Automapper.InitializeAutomapper();
                return mapper.Map<Administration, BLAdministrator>(Admin);
            }
            return new BLAdministrator();
        }
        public async Task<List<BLAdministrator>> GetAllSecondaryAdmins()
        {
            List<Administration> ListOfAdmins = await _administrationRepository.GetSecondaryAdministrators();
            Console.WriteLine(ListOfAdmins.Count);
            var mapper = Automapper.InitializeAutomapper();
            return mapper.Map<List<Administration>, List<BLAdministrator>>(ListOfAdmins);
        }
        public async Task<bool> DeleteAdmin(Guid Deletedby, Guid SecondaryAdminId)
        {
            if (SecondaryAdminId != null)
            {
                return await _administrationRepository.UpdateDeletedByAndIsActive(Deletedby, SecondaryAdminId);
            }
            return false;
        }
        public async Task<BLAdministrator> UpdateAdministrator(BLAdministrator secondaryAdmin)
        {
            if (secondaryAdmin is not null)
            {
                var mapper = Automapper.InitializeAutomapper();
                Administration Admin = await _administrationRepository.UpdateAdministrator(mapper.Map<BLAdministrator, Administration>(secondaryAdmin));
                return secondaryAdmin;
            }
            return new BLAdministrator();
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
                    if (await _accountCredentialsRepository.IsValidCredential(Admin.AccountCredentialsId, password))
                    {
                        Console.WriteLine(Admin.PhoneNumber);
                        return mapper.Map<BLAdministrator>(Admin);
                    }
                }
                return new BLAdministrator();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLAdministrator>> AdminsCreatedByAdmin(Guid AdminId)
        {
            if (AdminId != null)
            {
                List<Administration> CreatedAdmins = await _administrationRepository.GetCreatedAdministratorsById(AdminId);
                var mapper = Automapper.InitializeAutomapper();
                return mapper.Map<List<Administration>, List<BLAdministrator>>(CreatedAdmins);
            }
            return new List<BLAdministrator>();
        }
    }
}
