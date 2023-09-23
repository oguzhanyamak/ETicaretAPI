using ETicaretAPI.Application.Abstraction.GoogleAuth;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.GoogleAuth
{
    public class GoogleAuthService : IGoogleAuth
    {
        public async Task<string> Login(string IdToken)
        {
            var settings  = new GoogleJsonWebSignature.ValidationSettings()
            {
              Audience = new List<string> { "243490592105-np1s5vppcjlchilclnauugtqmsn1htte.apps.googleusercontent.com" }
            };
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(IdToken,settings);
            return payload.Subject;
        }
    }
}
