using System.Collections.Generic;

namespace CodeGenerator.Infrastructure
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
        public int CurrentDb { get; set; }
    }
}
