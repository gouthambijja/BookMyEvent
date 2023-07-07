using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Services;
using BookMyEvent.DLL;
using BookMyEvent.DLL.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BookMyEvent.BLL
{
    public class BLLConfig
    {
       
        public static void BLLConfigure(IServiceCollection services, ConfigurationManager configuration)
        {
            DLLConfig.DLLConfigure(services, configuration);
            
            services.AddScoped<IOrganiserFormServices, OrganiserFormServices>();
            services.AddScoped<IOrganisationServices, OrganisationServices>();
            services.AddScoped<IOrganiserServices, Organiserservices>();
            services.AddScoped<ITicketServices,TicketServices>();
            services.AddScoped<IAdminService,AdminServices>();
            services.AddScoped<IUserInputFormService,UserInputFormService>();
            services.AddScoped<IAdminService,AdminServices>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ICategoryServices,CategoryServices>();
            services.AddScoped<IEventServices, EventServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            
        }
    }
}