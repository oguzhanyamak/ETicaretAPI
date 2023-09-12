using ETicaretAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductAsyncReadRepository _repository;
        public GetByIdProductQueryHandler(IProductAsyncReadRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var _product = await _repository.GetByIdAsync(request.Id);
            return new() {Name = _product.Name,Price = _product.Price,Stock = _product.Stock };
        }
    }
}
