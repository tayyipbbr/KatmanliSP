using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.Base
{
    public interface ITokenCreator
    {
        string GenerateToken(int userId, string username, List<string> roles);
        string HashPassword(string password);                                               // HashPassword SP ile çakışabilir, orada yalnızca Password olarak geçiyor.
    }
}
