using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Helpers
{
    public class ClaimsHelper
    {
        public static Claim[] GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Email, user.Emails?.FirstOrDefault().Address ?? string.Empty),
                new Claim(ClaimTypes.Name, user.Username),

            };
            if (user.Roles?.GlobalRoles != null)
            {
                var adminRole = user.Roles.GlobalRoles.Where(r => r.ToLower().Contains("admin")).First();
                claims.Add(new Claim(ClaimTypes.Role, adminRole));
            }
            else if (user.Roles?.Customer != null)
            {
                var role = user.Roles.Customer.First() ?? "Guest";
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Guest"));

            }
            return claims.ToArray();
        }
    }
}
