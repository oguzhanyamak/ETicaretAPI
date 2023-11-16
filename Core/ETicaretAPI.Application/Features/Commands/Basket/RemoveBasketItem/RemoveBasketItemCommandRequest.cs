using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Basket.RemoveBasketItem
{
    public class RemoveBasketItemCommandRequest : IRequest<RemoveBasketItemCommandResponse>
    {
        public string ProductId { get; set; }
    }
}