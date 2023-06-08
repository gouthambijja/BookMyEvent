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
        }
    }
}