﻿using ETicaretAPI.Application.DTOs.Authentication;
using ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ETicaretAPI.Application.Features.Commands.AppUser.LoginUser;
using ETicaretAPI.Application.Features.Commands.AppUser.RefreshTokenLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IAuthService : IExternalAuthService,IInternalAuthService
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken,string userId);

    }

    public interface IExternalAuthService
    {
        Task<GoogleLoginCommandResponse> GoogleLoginAsync(GoogleLoginCommandRequest request);
    }

    public interface IInternalAuthService
    {
        Task<LoginUserCommandResponse> LoginAsync(string email,string password);
        Task<LoginRefreshTokenCommandResponse> RefreshTokenLoginAsync(string refToken);
    }
}
