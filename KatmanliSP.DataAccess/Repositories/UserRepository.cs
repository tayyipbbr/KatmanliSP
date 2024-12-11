using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Entities;
using KatmanliSP.DataAccess.Context;

namespace KatmanliSP.DataAccess.Repositories
{
    public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public string CheckUserRole(string UserId)
        {
            throw new NotImplementedException();     // TODO: UserId'e göre Role yansıtır. -öylesine bir metot-
        }
    }
}
