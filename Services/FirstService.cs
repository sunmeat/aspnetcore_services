using aspnetcore_services.Services.Interfaces;

namespace aspnetcore_services.Services
{
    public class FirstService : IMyService
    {
        public Guid Id { get; } = Guid.NewGuid();

        public FirstService()
        {
            Console.WriteLine("FirstService instance initialized.");
        }

        public void Logic()
        {
            Console.WriteLine(Id);
        }
    }
}
