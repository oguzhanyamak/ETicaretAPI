using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.VerifyResetToken
{
    public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
    {
        public string resetToken { get; set; }
        public string userId { get; set; }
    }
}