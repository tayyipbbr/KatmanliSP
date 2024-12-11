using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Entities;

namespace KatmanliSP.DataAccess.Repositories
{
    public interface IProductRepository : IGeneticRepository<Product>
    {
        public List<Product> BestSeller(); // ?
    }
}
