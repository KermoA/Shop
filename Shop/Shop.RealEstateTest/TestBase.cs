

using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication.ExtendedProtection;

namespace Shop.RealEstateTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider {  get; set; }

        protected TestBase() 
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {

        }
    }
}
