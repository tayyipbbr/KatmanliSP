﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatmanliSP.Core.DTOs.UserDTO
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
