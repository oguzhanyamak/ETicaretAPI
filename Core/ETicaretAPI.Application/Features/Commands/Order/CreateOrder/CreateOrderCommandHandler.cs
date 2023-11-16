﻿using ETicaretAPI.Application.Abstraction.Hubs;
using ETicaretAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {

        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrder(new() { Address = request.Address,Description = request.Description,BasketId = _basketService.GetUserActiveBasket.Id.ToString()});
            _orderHubService.OrderCreatedMessageAsync("Sipariş Var");
            return new();
        }
    }
}
