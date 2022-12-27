using System.IdentityModel.Tokens.Jwt;

namespace SoloDevApp.Service.Helpers
{
    public interface IJwtReader
    {
        string GetUserIdFromToken(string token);
    }
    
    public class JwtReader : IJwtReader
    {
        public string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var identifier = handler.ReadJwtToken(token).Payload;
            var name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            return identifier[name].ToString();
        }
    }
}