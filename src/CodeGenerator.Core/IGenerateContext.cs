using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core
{
    public interface IGenerateContext
    {
        ICollection<ITable> GetTables();
        ICollection<IField> GetFields();
        ICollection<IKey> GetKeys();
    }
}
