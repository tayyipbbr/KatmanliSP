using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public class Response<T> : Result, IResponse<T>
    {
        public Response(bool issuccess, T data) : base(issuccess)
        {
            Data = data;
        }
        public Response(T data, string message, bool issuccess) : base(issuccess, message)
        {
            Data = data;
            Message = message;
            Issuccess = issuccess;
        }
        public Response(string message) : base(default, message)
        {
            Message = message;
        }
        public Response(bool issuccess, string message) : base(issuccess, message)
        {
            Issuccess = issuccess;
            Message = message;
        }
        public T Data { get; set; }
    }
}

/*
 *         public Response(bool issuccess, T data) : base(issuccess) //
        {
            Data = data;
        }

        public Response(bool issuccess, string message, T data) : base(issuccess, message) //
        {
            Data = data;
            Message = message;
            Issuccess = issuccess;
        }

        public Response(string message) : base(message) //
        {
            Message = message;
        }

        public Response(bool issuccess, string message) : base(issuccess, message) //
        {
            Issuccess = issuccess;
            Message = message;
        }

        public T Data { get; set; } //
 */