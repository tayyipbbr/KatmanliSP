using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Entities;
using KatmanliSP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace KatmanliSP.DataAccess.Repositories
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public List<Product> BestSeller()
        {
            throw new NotImplementedException();        // TODO: güncelle.
        }
    }
}
