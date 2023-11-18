using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
    {
        public string userId { get; set; }
        public string resetToken { get; set; }
        public string newPassword { get; set; }
    }
}