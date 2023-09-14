﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Kullanıcı Adı Veya Şifre Hatalı")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }
    }
}
