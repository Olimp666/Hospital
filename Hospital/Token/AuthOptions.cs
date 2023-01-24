using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hospital.Token
{
    public class AuthOptions
    {
        public const string ISSUER = "Hospital";
        public const string AUDIENCE = "Hospital";
        const string KEY = "verylongkey";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
