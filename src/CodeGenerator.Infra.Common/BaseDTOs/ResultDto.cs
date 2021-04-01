using System.Collections.Generic;

namespace CodeGenerator.Infra.Common.BaseDTOs
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResultDto<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeed { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 表单验证错误列表
        /// </summary>
        public List<string> Errors { get; set; }
        /// <summary>
        /// 数据内容
        /// </summary>
        public T Content { get; set; }
    }
}
