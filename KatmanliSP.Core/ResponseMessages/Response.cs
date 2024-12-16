using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public class Response<T> : Result, IResponse<T>
    {
        public Response(bool issuccess) : base(issuccess)
        {
        }

        public Response(string message) : base(message)
        {
        }

        public Response(bool issuccess, string message) : base(issuccess, message)
        {
        }

        public T Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}
