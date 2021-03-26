using CodeGenerator.Infra.Common.BaseEntities;
using MySql.Data.Types;

namespace CodeGenerator.Core.ForTest.Entities
{
    public class UserContact : Entity
    {
        public long UserId { get; set; }
        public string ContactAddress { get; set; }
        public string ContactTelephone { get; set; }
        public virtual User User { get; set; }
        public MySqlGeometry Geometry { get; set; }

    }
}
