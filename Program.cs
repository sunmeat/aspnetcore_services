using aspnetcore_services.Services;
using aspnetcore_services.Services.Interfaces;
using ServicesInMiddleware.Services;

namespace aspnetcore_services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            // реєстрація сервісу IMyService з реалізацією FirstService:

            // 1) Transient: при кожному DI-запиті на IMyService створюється новий екземпляр FirstService
            // в межах одного HTTP-запиту, якщо IMyService буде запрошено кілька разів, то кожен раз буде створюватися новий екземпляр FirstService
            builder.Services.AddTransient<IMyService, FirstService>();

            // 2) Scoped: при кожному HTTP-запиті створюється новий екземпляр FirstService, який буде використовуватися для всіх DI-запитів на IMyService протягом цього запиту
            // builder.Services.AddScoped<IMyService, FirstService>();

            // 3) Singleton: створюється один екземпляр FirstService на весь час життя додатку, який буде використовуватися для всіх DI-запитів на IMyService
            // перевірити можна, якщо зробити оновлення сторінки кілька разів - в консолі буде виводитися один і той же Id
            // builder.Services.AddSingleton<IMyService, FirstService>();

            // небажаний варіант реєстрації, оскільки нема гнучкості на рівні контролера, доведеться змінювати код контролера, якщо потрібно буде змінити реалізацію сервісу
            builder.Services.AddTransient<SecondService>();

            // красивий варіант реєстрації сервісів через розширювальні методи, які інкапсулюють логіку реєстрації сервісів
            builder.Services.AddMyCoolService();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
