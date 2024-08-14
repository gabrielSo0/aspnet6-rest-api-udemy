using RestAPI.Configurations;
using System.Security.Claims;

namespace RestAPI.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private TokenConfiguration _configuration;
        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
