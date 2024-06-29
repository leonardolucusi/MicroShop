using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MicroShop.Web.Utils
{
    public static class GetUserIdFromJWT
    {
        public static int GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();
            handler.InboundClaimTypeMap.Add(ClaimTypes.NameIdentifier, "nameid");
            var jwtToken = handler.ReadJwtToken(token);
            var userIdString = jwtToken.Claims.First(claim => claim.Type == "nameid").Value;
            if (int.TryParse(userIdString, out int userId))
            {
                return userId;
            }
            return userId;
        }
    }
}
