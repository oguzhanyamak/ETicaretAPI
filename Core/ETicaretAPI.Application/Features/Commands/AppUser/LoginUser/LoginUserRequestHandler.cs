using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserRequestHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserRequestHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginUserErrorCommandResponse() { Succeded = false, Message = "Kullanıcı Adı Veya Şifre Hatalı" };
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                Token token = _tokenHandler.CreateAccessToken(15);
                return new LoginUserSuccessCommandResponse() { token = token, Message = "Başarılı", Succeded = result.Succeeded };
            }
            else
            {
                return new LoginUserErrorCommandResponse() { Succeded = result.Succeeded, Message = "Kullanıcı Adı Veya Şifre Hatalı" };
            }


        }
    }
}
