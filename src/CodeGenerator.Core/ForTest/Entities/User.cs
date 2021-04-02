using System;
using System.Collections.Generic;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.ForTest.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual ICollection<UserContact> UserContacts { get; set; }
    }
}
