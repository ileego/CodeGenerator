using System.Threading.Tasks;
using CodeGenerator.Core.Implements;

namespace CodeGenerator.Core.Interfaces
{
    public interface ITableFactory<in TDbTable, in TDbFields>
    {
        IGenerateContext GenerateContext { get; set; }
        Table CreateTable(TDbTable dbTable, TDbFields dbFields);
        Task<IGenerateContext> CreateContext();
    }
}
