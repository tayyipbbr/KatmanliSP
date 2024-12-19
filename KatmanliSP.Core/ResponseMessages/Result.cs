using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public class Result : IResult
    {
        public Result(bool issuccess)
        {
            Issuccess = issuccess;
        }

        public Result(string message)
        {
            Message = message;
        }

        public Result(bool issuccess, string message)
        {
            Message = message;
            Issuccess = issuccess;
        }

        public string Message { get; set; }
        public bool Issuccess { get; set; }

      //  public bool IsSuccess => throw new NotImplementedException();
    }
}
