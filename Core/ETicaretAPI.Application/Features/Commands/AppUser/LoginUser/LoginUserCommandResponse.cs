using ETicaretAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        public bool Succeded { get; set; }
        public string Message { get; set; }
    }


    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public Token token { get; set; }
    }

    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
    }

}
