using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Rolename { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }        // N-N
    }
}
