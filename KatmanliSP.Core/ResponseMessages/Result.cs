using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public class Result : IResult
    {
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(string message)
        {
            Message = message;
        }

        public Result(bool isSuccses, string message)
        {
            Message = message;
            IsSuccess = isSuccses;
        }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
