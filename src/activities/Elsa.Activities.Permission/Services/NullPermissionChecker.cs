using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elsa.Activities.Permission.Services
{
    public class NullPermissionChecker : IPermissionChecker
    {
        public Task<bool> IsInUsers(IEnumerable<string> users)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsInRoles(IEnumerable<string> roles)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsInDepartments(IEnumerable<string> departments)
        {
            return Task.FromResult(true);
        }
    }
}