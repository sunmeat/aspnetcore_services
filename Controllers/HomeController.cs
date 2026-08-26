using aspnetcore_services.Services;
using aspnetcore_services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_services.Controllers
{
    public class HomeController : Controller
    {
        private IMyService fieldService;
        // private SecondService secondService;

        // при кожному запиті, контролер створюється заново, тому поле fieldService буде ініціалізовано новим екземпляром FirstService при кожному запиті
        public HomeController(IMyService diService) // Dependency Injection of IMyService via constructor
        {
            this.fieldService = diService;
        }

        public IActionResult Index([FromServices] IMyService paramService) // Dependency Injection of IMyService via method parameter
        {
            fieldService.Logic();

            paramService.Logic();

            var reqService = HttpContext.RequestServices.GetRequiredService<IMyService>(); // Dependency Injection of IMyService via HttpContext.RequestServices
            reqService.Logic();

            return Content("результати дивіться в консолі");
        }
    }
}
