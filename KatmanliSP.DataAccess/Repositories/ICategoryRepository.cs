using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Base;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.Entities;

namespace KatmanliSP.DataAccess.Repositories
{
    public interface ICategoryRepository : IGeneticRepository<Category>
    {
        public List<Category> BestSeller(int topX);
    }
}
