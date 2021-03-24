using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.BaseEntities;

namespace CodeGenerator.Infra.Common.ForTest.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual ICollection<UserContact> UserContacts { get; set; }
    }
}
