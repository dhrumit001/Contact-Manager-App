using App.Data;
using App.Data.Repository;
using App.Services.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace LearnProject.Services.Tests
{
   
    public class ServiceTestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }


        private void ConfigureServices(IServiceCollection services)
        {
            // Build DbContextOptions
            services.AddDbContext<ObjectContext>(options =>
             options.UseInMemoryDatabase(databaseName: "ContactManagerTestDb"));

            // Add other services
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        }
    }
}
