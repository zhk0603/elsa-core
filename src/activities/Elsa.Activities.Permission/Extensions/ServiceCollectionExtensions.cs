using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Activities.Permission.Extensions
{
    public static class ServiceCollectionExtensions
    {        
        public static IServiceCollection AddPermissionActivities(this IServiceCollection services)
        {
            return services
                .AddActivity<Activities.Permission>();
        }
    }
}