using LinkShortener.BLL;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class AuthTokenModel
    {
        public string ISSUER { get; set; } = "Link shortener";
        public string AUDIENCE { get; set; } = "Link shortener client";
        public static string KEY { get; set; } = Helpers.GetAlphaNumString(16);
        public static int LIFETIME { get; set; } = 30;
    }
}
