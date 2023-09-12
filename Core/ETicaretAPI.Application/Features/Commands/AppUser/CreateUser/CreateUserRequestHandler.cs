﻿using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserRequestHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Ad = request.Name,
                Soyad = request.Surname,
                Email = request.Email,
                UserName = request.Email,
            },request.Password);

            CreateUserCommandResponse response = new CreateUserCommandResponse() { Succeeded = result.Succeeded};
            if (result.Succeeded)
            {
                response.Message = "Kullanıcı Başarıyla Oluşturulmuştur";
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    response.Message += $"{error.Code}-{error.Description}";
                }
            }
            return response;

        }
    }
}
