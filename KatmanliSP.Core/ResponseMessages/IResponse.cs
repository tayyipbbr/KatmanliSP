﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.ResponseMessages
{
    public interface IResponse<T> : IResult
    {
        T Data { get; set; }
    }
}
