using ETicaretAPI.Application.Abstraction.GoogleAuth;
using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.Authentication;
using ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ETicaretAPI.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly IGoogleAuth _googleAuth;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

        public AuthService(UserManager<Domain.Entities.Identity.AppUser> userManager, IGoogleAuth googleAuth, ITokenHandler tokenHandler, SignInManager<Domain.Entities.Identity.AppUser> signInManager)
        {
            _userManager = userManager;
            _googleAuth = googleAuth;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
        }


        public async Task<GoogleLoginCommandResponse> GoogleLoginAsync(GoogleLoginCommandRequest request)
        {
            string subject = await _googleAuth.Login(request.IdToken);
            var info = new UserLoginInfo(request.Provider, subject,request.Provider);
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
                        return new() { Message = "Kayıt Başarılı", Succeded = result.Succeeded, token = token };
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

        public async Task<LoginUserCommandResponse> LoginAsync(string email,string password)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new LoginUserErrorCommandResponse() { Succeded = false, Message = "Kullanıcı Adı Veya Şifre Hatalı" };
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
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
