using ETicaretAPI.Application.Abstraction.GoogleAuth;
using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginRequestHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly IGoogleAuth _googleAuth;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginRequestHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, IGoogleAuth googleAuth, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _googleAuth = googleAuth;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string subject = await _googleAuth.Login(request.IdToken);
            var info = new UserLoginInfo(request.Provider, subject, request.Provider);
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = request.Email,
                        UserName = request.Email,
                        Ad = request.FirstName,
                        Soyad = request.LastName,
                    };
                    IdentityResult result = await _userManager.CreateAsync(user);

                    if (result.Succeeded)
                    {
                        await _userManager.AddLoginAsync(user, info);
                        Token token = _tokenHandler.CreateAccessToken(15);
                        return new() { Message = "Kayıt Başarılı", Succeded = result.Succeeded ,token = token};
                    }
                    else
                    {
                        return new() { Message = "Kayıt Başarısız", Succeded = result.Succeeded };
                    }

                }
                else
                {
                    return new() { Message = "Kayıt Başarısız", Succeded = false };
                }
            }
            else
            {
                return new() { Message = "Kayıt Başarısız", Succeded = false };
            }

        }
    }
}
