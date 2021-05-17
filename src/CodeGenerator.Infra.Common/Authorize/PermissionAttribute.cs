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
        /// 分组标识
        /// </summary>
        public string GroupTag { get; private set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; private set; }
        /// <summary>
        /// Action标识
        /// </summary>
        public string ActionTag { get; private set; }
        /// <summary>
        /// Action名称
        /// </summary>
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
