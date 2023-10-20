using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByName
{
    public class GetProductsByNameRequest : IRequest<GetProductsByNameResponse>
    {
        public string ProdName { get; set; }
    }
}
