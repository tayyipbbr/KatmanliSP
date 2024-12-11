using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.Entities
{
    public class User : BaseEntity
    {
       // public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
