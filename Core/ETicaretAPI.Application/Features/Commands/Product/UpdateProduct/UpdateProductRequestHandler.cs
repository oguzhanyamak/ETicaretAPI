using ETicaretAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductAsyncReadRepository _readRepository;
        private readonly IProductAsyncWriteRepository _writeRepository;
        public UpdateProductRequestHandler(IProductAsyncReadRepository repository, IProductAsyncWriteRepository writeRepository)
        {
            _readRepository = repository;
            _writeRepository = writeRepository;
        }
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _readRepository.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            await _writeRepository.Save();
            return new() { };

        }
    }
}
