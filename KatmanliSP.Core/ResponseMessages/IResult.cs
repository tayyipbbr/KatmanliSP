using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public interface IResult
    {
        public string Message { get; }
        public bool Issuccess { get; }
    }
}
