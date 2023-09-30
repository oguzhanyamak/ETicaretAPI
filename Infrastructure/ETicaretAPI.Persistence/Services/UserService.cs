using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUser user)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Ad = user.Name,
                Soyad = user.Surname,
                Email = user.Email,
                UserName = user.Email,
            }, user.Password);

            CreateUserResponse response = new CreateUserResponse() { Succeeded = result.Succeeded };
            if (result.Succeeded)
            {
                response.Message = "Kullanıcı Başarıyla Oluşturulmuştur";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code}-{error.Description}";
                }
            }
            return response;
        }

        public async Task UpdateRefreshToken(string refToken,AppUser user,DateTime AccessTokenEndDate)
        {

                user.RefreshToken = refToken;
                user.RTEndDate = AccessTokenEndDate.AddMinutes(5);
                await _userManager.UpdateAsync(user);
        }
    }
}
