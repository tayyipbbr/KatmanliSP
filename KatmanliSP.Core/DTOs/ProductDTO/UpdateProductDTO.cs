using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Entities;

namespace KatmanliSP.Core.DTOs.ProductDTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        [DefaultValue("")]
        public string Name { get; set; }
        [DefaultValue("")]
        public string Description { get; set; }
        [DefaultValue(-1)]
        public int InStock { get; set; }
        //public Category CategoryId { get; set; }
    }
}


