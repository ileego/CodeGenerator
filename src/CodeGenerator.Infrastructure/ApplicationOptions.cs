using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infrastructure
{
    /// <summary>
    /// 应用
    /// </summary>
    public class ApplicationOptions
    {
        /// <summary>
        /// Application Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Application Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Application Name
        /// </summary>
        public string Name { get; set; }
    }
}
