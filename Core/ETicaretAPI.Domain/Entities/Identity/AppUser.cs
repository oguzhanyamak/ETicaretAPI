﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Identity
{
    public class AppUser :IdentityUser<string>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RTEndDate { get; set; }
        public ICollection<Basket> Baskets { get; set; }

    }
}
