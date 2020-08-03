using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elsa.Activities.Permission.Services
{
    public interface IPermissionChecker
    {
        Task<bool> IsInUsers(IEnumerable<string> users);
        Task<bool> IsInRoles(IEnumerable<string> roles);
        Task<bool> IsInDepartments(IEnumerable<string> departments);
    }
}