using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUser user);
        Task UpdateRefreshTokenAsync(string refToken,AppUser user, DateTime AccessTokenEndDate);

        Task UpdatePasswordAsync(string userId,string resetToken,string newPassword);
        
    }
}
