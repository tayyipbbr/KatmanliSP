using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Service.Security
{
    public interface ITokenCreator
    {
        string GenerateToken(string username, int userid, int roleid); 
        string GenerateHashPassword(string password);      // HashPassword'idi.  // HashPassword SP ile çakışabilir, orada yalnızca Password olarak geçiyor.
    }
}
