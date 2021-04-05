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
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; private set; }
        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { get; private set; }

        public PermissionAttribute(string groupName, string actionName)
            : base(Permission.Policy)
        {
            this.GroupName = groupName;
            this.ActionName = actionName;
        }
    }
}
