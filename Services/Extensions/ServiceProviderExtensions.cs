using aspnetcore_services.Services;
using aspnetcore_services.Services.Interfaces;

namespace ServicesInMiddleware.Services
{
    // розширювальний метод для підключення сервіса красивим способом - builder.Services.AddMyCoolService();

    // !!! зазвичай такі класи розміщують у окремих файлах
    // цей клас підключає потрібний сервіс з урахуванням його життєвого циклу (AddTransient, AddScoped, AddSingleton)
    // саме він дозволяє використовувати метод builder.Services.AddMyCoolService у Program.cs
    // IServiceCollection services == builder.Services в Program.cs
    // без нього довелось би писати builder.Services.AddTransient<IMyService, FirstService>(); - нудно і громіздко
    public static class ServiceProviderExtensions
    {
        // розширювальний метод для IServiceCollection, який додає наш сервіс FirstService як реалізацію інтерфейсу IMyService
        // якщо підзабули, що таке розширювальний метод - гляньте https://gist.github.com/sunmeat/75d1693cb6e23e7979c8701b116718c1
        public static void AddMyCoolService(this IServiceCollection services)
        {
            services.AddTransient<IMyService, FirstService>();
        }
    }
}
