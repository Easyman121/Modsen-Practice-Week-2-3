using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Authentification
{
    public class AuthConfiguration
    {
        public const string ISSUER = "BNTU1";
        public const string AUDIENCE = "OurClients";
        const string KEY = "thiskeyisnotsupposedtobeherebutitsjustpracticesoyeah";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
