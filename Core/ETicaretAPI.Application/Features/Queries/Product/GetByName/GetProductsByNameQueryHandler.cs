using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByName
{
    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameRequest, GetProductsByNameResponse>
    {
        private readonly IProductReadRepository _repository;

        public GetProductsByNameQueryHandler(IProductReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetProductsByNameResponse> Handle(GetProductsByNameRequest request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetWhere(x => x.Name.Contains(request.ProdName)).ToListAsync();
            if(products is not null)
            {
                return new() {Products = products };
            }
            return new() { };
        }
    }
}
