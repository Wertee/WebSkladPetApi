using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication
{
    public class AuthenticationOption
    {
        public string Issuer { get; set; } // Who generated token
        public string Audience { get; set; } // For whom
        public string Secret { get; set; } // Secret string
        public int TokenLifetime { get; set; } // seconds

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
