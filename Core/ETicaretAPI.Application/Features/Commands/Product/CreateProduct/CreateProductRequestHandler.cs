using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductAsyncWriteRepository _productAsyncWriteRepository;
        public CreateProductRequestHandler(IProductAsyncWriteRepository productAsyncWriteRepository)
        {
            _productAsyncWriteRepository = productAsyncWriteRepository;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = new() { Name = request.Name, Stock = request.Stock, Price = request.Price };
            await _productAsyncWriteRepository.AddAsync(product);
            await _productAsyncWriteRepository.Save();
            return new() { };
        }
    }
}
