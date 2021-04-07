using System.Collections.Generic;

namespace CodeGenerator.Infra.Common.Options
{
    public class RedisOptions
    {
        /// <summary>
        /// the master server
        /// </summary>
        public string MasterServer { get; set; }

        /// <summary>
        /// the slave server
        /// </summary>
        public List<string> SlaveServer { get; set; }

        /// <summary>
        /// select redis db
        /// </summary>
        public int DefaultDatabase { get; set; }
    }
}
