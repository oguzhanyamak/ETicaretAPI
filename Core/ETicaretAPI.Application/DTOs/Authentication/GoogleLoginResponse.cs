using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Authentication
{
    public class GoogleLoginResponse
    {
        public bool Succeded { get; set; }
        public string Message { get; set; }
        public Token? token { get; set; }
    }
}
