using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEIIApp.Server.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEIIApp.Server.Test {
    public class ServiceHelper {

        public static IServiceCollection GetConfiguredServiceCollection() {
            var sc = CreateServiceCollection();
            ConfigureServiceCollection(sc);
            return sc;
        }

        private static IServiceCollection CreateServiceCollection() {
            return new ServiceCollection();
        }

        private static void ConfigureServiceCollection(IServiceCollection services) {
            
            services.AddDbContext<DatabaseContext>(options => {
                options.UseInMemoryDatabase("InMemoryDb");
            });

            services.AddAutoMapper(typeof(Domain.DomainMapper));

            //Add other services that should be added to the collection
            services.AddScoped<Services.QuizDefinitionService>();
        }

        private static IServiceProvider GetServiceProvider(IServiceCollection serviceCollection) {
            var provider = new DefaultServiceProviderFactory().CreateServiceProvider(serviceCollection);
            return provider;
        }

        private static IServiceScope GetScopedServiceProvider(IServiceProvider serviceProvider) {
            var scope = serviceProvider.CreateScope();
            return scope;
        }

        public static IServiceScope CreateServiceScope(IServiceCollection collection) {
            var provider = GetServiceProvider(collection);
            var scope = GetScopedServiceProvider(provider);
            return scope;
        }


    }
}
