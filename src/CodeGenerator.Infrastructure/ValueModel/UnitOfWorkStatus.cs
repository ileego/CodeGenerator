using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infrastructure.ValueModel
{
    public class UnitOfWorkStatus
    {
        public bool IsStartingUow { get; internal set; }
    }
}
