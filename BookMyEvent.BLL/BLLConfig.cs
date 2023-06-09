using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Services;
using BookMyEvent.DLL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BookMyEvent.BLL
{
    public class BLLConfig
    {
        public static void BLLConfigure(IServiceCollection services, ConfigurationManager configuration)
        {
            DLLConfig.DLLConfigure(services, configuration);
            services.AddScoped<ITicketServices,TicketServices>();
            services.AddScoped<IUserInputFormService,UserInputFormService>();
        }
    }
}