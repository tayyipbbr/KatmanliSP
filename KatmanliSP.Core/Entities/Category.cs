using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; } // açıklama
        public int? ParentCategoryId { get; set; }
        public ICollection<Product> Products { get; set; } // bire çok ilişki
    }
}
