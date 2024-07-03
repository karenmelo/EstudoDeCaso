using EstudoDeCaso.Infra.Repositories;
using EstudoDeCaso.Infra.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EstudoDeCaso.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region SERVICES
            var serviceAssembly = typeof(IHelperService).Assembly;
            var registrationsService = from type in serviceAssembly.GetExportedTypes()
                                       where type.Namespace.StartsWith("EstudoDeCaso.Infra.Services")
                                       from service in type.GetInterfaces()
                                       select new { service, implementation = type };

            foreach (var registration in registrationsService)
            {
                services.AddScoped(registration.service, registration.implementation);
            }

            #endregion

            #region REPOSITORIES
            var assembly = typeof(ProdutoRepository).Assembly;
            var typesToRegister = assembly.GetExportedTypes()
                                          .Where(type => type.Namespace == "EstudoDeCaso.Infra.Repositories");

            foreach (var type in typesToRegister)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(p => !p.FullName.Contains("IReposit"));
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
                else
                    services.AddScoped(type);
            }

            #endregion

            return services;
        }
    }
}
