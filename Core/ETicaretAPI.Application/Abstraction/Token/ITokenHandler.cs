﻿using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(AppUser user,int minute = 15);
        string CreateRefreshToken(int minute);

    }
}
