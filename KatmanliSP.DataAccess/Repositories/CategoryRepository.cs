using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.Entities;
using KatmanliSP.DataAccess.Context;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// SQLQuery2

namespace KatmanliSP.DataAccess.Repositories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public List<Category> BestSeller(int topX)
        {
            throw new NotImplementedException();     // TODO: inputa göre max satılan ilk X değeri liste halinde verir.
        }
    }
}


