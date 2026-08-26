using aspnetcore_services.Services;
using aspnetcore_services.Services.Interfaces;

namespace ServicesInMiddleware.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddMyCoolService(this IServiceCollection services)
        {
            services.AddTransient<IMyService, FirstService>();
        }

        public static void AddMyYetAnotherService(this IServiceCollection services)
        {
            services.AddTransient<SecondService>();
        }
    }
}
