using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs
{
    public class Token //Token Modeli
    {
        public string AccessToken { get; set; }
        public DateTime Expration { get; set; }
    }
}
