using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Basket.UpdateQuantity
{
    public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}