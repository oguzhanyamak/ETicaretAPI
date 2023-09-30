using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class LoginRefreshTokenCommandResponse
    {
        public bool Succeded { get; set; }
        public string Message { get; set; }
        public Token? token { get; set; }
    }
}
