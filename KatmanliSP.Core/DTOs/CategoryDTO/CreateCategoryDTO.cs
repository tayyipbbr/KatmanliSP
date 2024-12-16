using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Entities;

namespace KatmanliSP.Core.DTOs.CategoryDTO
{
    public class CreateCategoryDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } // açıklama
        public DateTime CreateDate { get; set; }
    }
}
