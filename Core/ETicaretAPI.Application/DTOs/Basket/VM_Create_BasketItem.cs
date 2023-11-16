using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Basket
{
    public class VM_Create_BasketItem
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }

    }
}
