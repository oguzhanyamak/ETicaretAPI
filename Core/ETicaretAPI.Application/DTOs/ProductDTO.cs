using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs
{
    public class ProductDTO
    {
        public int Stock { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
    }
}
