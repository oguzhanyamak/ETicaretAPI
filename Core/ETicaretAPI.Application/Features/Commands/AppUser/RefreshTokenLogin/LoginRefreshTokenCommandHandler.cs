using ETicaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class LoginRefreshTokenCommandHandler : IRequestHandler<LoginRefreshTokenCommandRequest, LoginRefreshTokenCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginRefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginRefreshTokenCommandResponse> Handle(LoginRefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            LoginRefreshTokenCommandResponse response =await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return response;
        }
    }
}
