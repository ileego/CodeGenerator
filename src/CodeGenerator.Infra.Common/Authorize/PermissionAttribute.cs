using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infra.Common.Authorize
{
    public class Permission
    {
        public static readonly string Policy = "CheckPermission";
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string GroupTag { get; private set; }
        public string GroupName { get; private set; }
        public string ActionTag { get; private set; }
        public string ActionName { get; private set; }

        public PermissionAttribute(string groupTag, string groupName, string actionTag, string actionName)
            : base(Permission.Policy)
        {
            this.GroupTag = groupTag;
            this.GroupName = groupName;
            this.ActionTag = actionTag;
            this.ActionName = actionName;
        }
    }
}
