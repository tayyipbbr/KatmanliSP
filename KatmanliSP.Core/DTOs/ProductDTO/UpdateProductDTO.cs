using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Entities;

namespace KatmanliSP.Core.DTOs.ProductDTO
{
    public class UpdateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int InStock { get; set; }
        public Category CategoryId { get; set; }
    }
}
