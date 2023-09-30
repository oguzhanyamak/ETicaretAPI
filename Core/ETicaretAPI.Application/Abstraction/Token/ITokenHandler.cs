﻿using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int minute = 15);
        string CreateRefreshToken(int minute);

    }
}
