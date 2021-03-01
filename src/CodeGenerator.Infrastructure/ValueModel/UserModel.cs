using System;
using System.Collections.Generic;

namespace CodeGenerator.Infrastructure.ValueModel
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// 附加内容
        /// </summary>
        public Dictionary<string, object> AdditionalContent { get; set; }
    }
}
