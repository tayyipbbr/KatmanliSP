using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public class Successful<T>  : Response<T>
    {
        public Successful(T data) : base(true, data)
        {
        }

        public Successful(T data, string message) : base(data, message, true)
        {
        }

        public Successful(string message) : base(default, message, true)
        {
        }

    }
}


