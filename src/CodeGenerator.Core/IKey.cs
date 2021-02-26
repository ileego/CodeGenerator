using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core
{
    public enum KeyEnum{
        PrimaryKey = 0,
        ForeignKey = 1
    }
    public interface IKey
    {
        public KeyEnum KeyType { get; set; }
        public string KeyName { get; set; }
    }
}
