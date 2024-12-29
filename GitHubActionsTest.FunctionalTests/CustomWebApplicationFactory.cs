using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubActionsTest.FunctionalTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly Action<IServiceCollection>? _configureServices;

        public CustomWebApplicationFactory(Action<IServiceCollection>? configureServices = null)
        {
            _configureServices = configureServices;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                _configureServices?.Invoke(services);
            });
        }
    }
}
