using ETicaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        readonly IUserService userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await userService.UpdatePasswordAsync(request.userId, request.resetToken, request.newPassword);
                return new();
            }
            catch (Exception)
            {
                return new();
            }
            
        }
    }
}
