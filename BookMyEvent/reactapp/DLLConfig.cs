using BookMyEvent.DLL.Contracts;
using BookMyEvent.DLL.Repositories;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookMyEvent.DLL
{
    public class DLLConfig
    {
        public static void DLLConfigure(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<EventManagementSystemTeamZealContext>(
                            
                            options => {
                                options.EnableSensitiveDataLogging();
                                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                            }
                            );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IFieldTypeRepository,FieldTypeRepository>();
            services.AddScoped<IEventCategoryRepository,EventCategoryRepository>();
            services.AddScoped<IAccountCredentialsRepository,AccountCredentialRepository>();
            services.AddScoped<IAdministrationRepository, AdministrationRepository>();
            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            services.AddScoped<IUserInputFormFieldsRepository, UserInputFormFieldsRepository>();
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IUserInputFormRepository, UserInputFormRepository>();
            services.AddScoped<IRegistrationFormFieldRepository, RegistrationFormFieldRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IEventRepository,EventRepository>();
            services.AddScoped<IEventImageRepository, EventImageRepository>();
        }
    }
}