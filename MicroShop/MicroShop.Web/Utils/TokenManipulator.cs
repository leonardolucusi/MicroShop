using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MicroShop.Web.Utils
{
    public static class TokenManipulator
    {
        public static int GetUserIdFromToken(string token)
        {
            if(token is null) return 0;
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
        public static bool IsTokenExpired(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken.ValidTo < DateTime.UtcNow)
                return true;

            return false;
        }
    }
}
