using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.GoogleAuth
{
    public interface IGoogleAuth
    {
        public Task<string> Login(string IdToken);
    }
}
