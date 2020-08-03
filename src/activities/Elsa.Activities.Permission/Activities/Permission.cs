using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Activities.Permission.Results;
using Elsa.Activities.Permission.Services;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Results;
using Elsa.Scripting.JavaScript;
using Elsa.Services;
using Elsa.Services.Models;

namespace Elsa.Activities.Permission.Activities
{
    [ActivityDefinition(
        Category = "Permission",
        Description = "支持配置权限的活动。",
        Outcomes = new[] {OutcomeNames.Done, UnauthorizedResult.OutcomeName})]
    public class Permission : Activity
    {
        private readonly IPermissionChecker permissionChecker;

        public Permission(IPermissionChecker permissionChecker)
        {
            this.permissionChecker = permissionChecker;
        }

        [ActivityProperty(
            Label = "用户",
            Hint = "处理此活动的用户"
        )]
        public WorkflowExpression<IList<string>> Users
        {
            get => GetState(() => new JavaScriptExpression<IList<string>>("[]"));
            set => SetState(value);
        }

        [ActivityProperty(
            Label = "角色",
            Hint = "处理此活动的角色"
        )]
        public WorkflowExpression<IList<string>> Roles
        {
            get => GetState(() => new JavaScriptExpression<IList<string>>("[]"));
            set => SetState(value);
        }

        [ActivityProperty(
            Label = "部门",
            Hint = "处理此活动的部门"
        )]
        public WorkflowExpression<IList<string>> Departments
        {
            get => GetState(() => new JavaScriptExpression<IList<string>>("[]"));
            set => SetState(value);
        }

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var havePermission = await PermissionCheck(context);
            if (!havePermission)
            {
                return new UnauthorizedResult();
            }

            return Halt(); // 到达此环节后阻断。
        }

        protected override async Task<ActivityExecutionResult> OnResumeAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var havePermission = await PermissionCheck(context);
            if (!havePermission)
            {
                return new UnauthorizedResult();
            }

            return Done();
        }

        protected virtual async Task<bool> PermissionCheck(WorkflowExecutionContext context)
        {
            var users = await context.EvaluateAsync(Users, default);
            var roles = await context.EvaluateAsync(Roles, default);
            var departments = await context.EvaluateAsync(Departments, default);

            if (await permissionChecker.IsInUsers(users) ||
                await permissionChecker.IsInRoles(roles) ||
                await permissionChecker.IsInDepartments(departments))
            {
                return true;
            }

            return false;
        }
    }
}
