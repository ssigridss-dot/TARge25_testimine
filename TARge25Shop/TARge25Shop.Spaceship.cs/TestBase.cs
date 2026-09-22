using Microsoft.Extensions.DependencyInjection;
using TARge25Shop.ApplicationServices.Services;
using TARge25Shop.Core.ServiceInterface;

namespace TARge25Shop_SpaceshipTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }

        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);

        }

        public virtual void SetupServices(ServiceCollection services)
        {
            services.AddScoped<ISpaceshipServices, SpaceshipServices>();
        }
    }
}
