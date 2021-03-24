using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CodeGenerator.Infra.Common.BaseEntities;

namespace CodeGenerator.Infra.Common.ForTest.Entities
{
    public class UserContact : Entity
    {
        public long UserId { get; set; }
        public string ContactAddress { get; set; }
        public string ContactTelephone { get; set; }
        public virtual User User { get; set; }
    }
}
