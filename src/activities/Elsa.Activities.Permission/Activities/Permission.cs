using System;
using System.Collections.Generic;
using System.Text;
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
        Outcomes = new[] {OutcomeNames.Done})]
    public class Permission : Activity
    {
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

        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext workflowContext)
        {
            return Halt(); // 到达此环节后阻断。
        }

        protected override ActivityExecutionResult OnResume(WorkflowExecutionContext context)
        {
            return Done();
        }
    }
}
