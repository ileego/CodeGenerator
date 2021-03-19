using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infra.Common.ValueModel
{
    public class UnitOfWorkStatus
    {
        public bool IsStartingUow { get; internal set; }
    }
}
