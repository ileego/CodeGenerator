using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Infra.Common.Context
{
    /// <summary>
    /// 用户存储当前用户上下文
    /// </summary>
    public class UserContext
    {
        public string UserKey { get; set; }
        public UserModel User { get; set; }
    }
}
