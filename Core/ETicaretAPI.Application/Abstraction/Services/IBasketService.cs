using ETicaretAPI.Application.DTOs.Basket;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemAsync();
        public Task AddItemToBasketAsync(VM_Create_BasketItem item);
        public Task UpdateQuantityAsync(VM_Update_BasketItem item);
        public Task RemoveBasketItemAsync(string Id);

        public Basket GetUserActiveBasket {  get; }

    }
}
