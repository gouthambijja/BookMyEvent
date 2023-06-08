using BookMyEvent.BLL;

namespace BookMyEvent.WebApi
{
    public class Startup
    {
        public static void StartUpConfigure(IServiceCollection services, ConfigurationManager configuration)
        {
            BLLConfig.BLLConfigure(services, configuration); 
        }
    }
}
