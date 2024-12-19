using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public class Failed<T> : Response<T>
    {
        public Failed(T data) : base(false, data)
        {
        }

        public Failed(T data, string message) : base(data, message, false)
        {
        }

        public Failed(string message) : base(default, message, false)
        {
        }
    }
}
