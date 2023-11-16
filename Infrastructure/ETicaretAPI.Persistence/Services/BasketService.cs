using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.DTOs.Basket;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Application.Repositories.BasketItems;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketAsyncWriteRepository _basketAsyncWriteRepository;
        private readonly IBasketReadAsyncRepository _basketReadAsyncRepository;
        private readonly IBasketItemAsyncWriteRepository _basketItemAsyncWriteRepository;
        private readonly IBasketItemAsyncReadRepository _basketItemAsyncReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public BasketService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketAsyncWriteRepository basketAsyncWriteRepository, IBasketItemAsyncWriteRepository basketItemsAsyncWriteRepository, IBasketItemAsyncReadRepository basketItemsAsyncReadRepository, IBasketReadAsyncRepository basketReadAsyncRepository, IBasketItemWriteRepository basketItemWriteRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketAsyncWriteRepository = basketAsyncWriteRepository;
            _basketItemAsyncWriteRepository = basketItemsAsyncWriteRepository;
            _basketItemAsyncReadRepository = basketItemsAsyncReadRepository;
            _basketReadAsyncRepository = basketReadAsyncRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }



        private async Task<Basket?> ContextUser()
        {
            var username = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await _userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                var _basket = from basket in user.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = order
                              };

                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                {
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }
                await _basketAsyncWriteRepository.Save();

                return targetBasket;
            }
            throw new Exception("Beklenmeyen Bir Hata Oluştu");
        }

        public async Task AddItemToBasketAsync(VM_Create_BasketItem item)
        {
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItem basketItem = await _basketItemAsyncReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(item.ProductId));

                if (basketItem != null)
                {
                    basketItem.Quantity++;
                }
                else
                {
                    BasketItem basketitem = new BasketItem()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(item.ProductId),
                        Quantity = item.Quantity
                    };
                    await _basketItemAsyncWriteRepository.AddAsync(basketitem);
                }
                await _basketItemAsyncWriteRepository.Save();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemAsync()
        {
            Basket? basket = await ContextUser();

            Basket? result = await _basketReadAsyncRepository.Table.Include(b => b.BasketItems).ThenInclude(bi => bi.Product).FirstOrDefaultAsync(b => b.Id == basket.Id);
            return result.BasketItems.ToList();
        }

        public async Task RemoveBasketItemAsync(string Id)
        {
            BasketItem? basketItem = await _basketItemAsyncReadRepository.GetByIdAsync(Id);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemAsyncWriteRepository.Save();
            }
        }

        public async Task UpdateQuantityAsync(VM_Update_BasketItem item)
        {
            BasketItem? basketItem = await _basketItemAsyncReadRepository.GetByIdAsync(item.BasketItemId);
            if (basketItem != null)
            {
                basketItem.Quantity = item.Quantity;
                await _basketItemAsyncWriteRepository.Save();
            }
        }

        public Basket GetUserActiveBasket
        {
            get
            {
                Basket? basket = ContextUser().Result;
                return basket;
            }
        }
    }
}
