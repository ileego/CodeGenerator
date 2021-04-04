using System.Threading.Tasks;
using CodeGenerator.Core.Implements;

namespace CodeGenerator.Core.Interfaces
{
    public interface IGenerateContextBuilder<in TDbTable, in TDbFields>
    {
        IGenerateContext GenerateContext { get; set; }
        Task<IGenerateContext> BuildContext();
    }
}
