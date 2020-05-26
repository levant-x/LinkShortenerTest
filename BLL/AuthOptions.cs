using LinkShortener.BLL;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public static class AuthOptions
    {
        public const string ISSUER = "LinkShortener";
        public const string AUDIENCE = "LinkShortenerClient";
        public const int LIFETIME = 30;
        public static string KEY { get; set; } = Helpers.GetAlphaNumString(16);


        public static SymmetricSecurityKey CreateSymmSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
