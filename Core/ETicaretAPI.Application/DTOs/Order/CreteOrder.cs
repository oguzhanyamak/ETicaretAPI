﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string Address { get; set; }
        public string? BasketId { get; set; }
        public string Description { get; set; }
    }
}
