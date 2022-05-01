using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Application.Constants
{
    public class JwtTokenConstants
    {
        public static SecurityKey IssuerSigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GetCryptoSecurityKey()));

        public static SigningCredentials SigningCredentials => new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

        public static TimeSpan TokenExpirationTime => TimeSpan.FromDays(1);

        public static string Issuer => "Issuer";

        public static string Audience => "Audience";

        private static string GetCryptoSecurityKey()
        {
            var securityKey = "!_*19ASREAFATSUMZAMKROK07*_!";  //use key vault here

            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(securityKey));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
