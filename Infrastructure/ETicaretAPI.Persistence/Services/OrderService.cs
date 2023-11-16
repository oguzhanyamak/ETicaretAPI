using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderAsyncWriteRepository _repository;
        private readonly IOrderAsyncWriteRepository _writeRepository;

        public OrderService(IOrderAsyncWriteRepository repository, IOrderAsyncWriteRepository writeRepository)
        {
            _repository = repository;
            _writeRepository = writeRepository;
        }

        public async  Task CreateOrder(CreateOrder order)
        {
            Order _order = new();
            _order.Address = order.Address;
            _order.Description = order.Description;
            _order.Basket.Id = Guid.Parse(order.BasketId);
            await _repository.AddAsync(_order);
            await _writeRepository.Save();
        }
    }
}
