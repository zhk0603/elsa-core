using Elsa.Activities.Permission.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Elsa.Activities.Permission.Extensions
{
    public static class ServiceCollectionExtensions
    {        
        public static IServiceCollection AddPermissionActivities(this IServiceCollection services)
        {
            services
                .AddActivity<Activities.Permission>()
                .TryAddTransient<IPermissionChecker, NullPermissionChecker>();

            return services;
        }
    }
}