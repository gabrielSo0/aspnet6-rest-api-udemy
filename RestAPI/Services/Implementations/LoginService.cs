using RestAPI.Configurations;
using RestAPI.Data.VO;
using RestAPI.Model;
using RestAPI.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestAPI.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly TokenConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginService(TokenConfiguration configuration, IUserRepository userRepository, ITokenService tokenService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public TokenVO ReturnUserToken (UserVO userCredentials)
        {
            var user = _userRepository.ValidateCredentials(userCredentials);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            UpdateUserInfo(user, refreshToken);

            DateTime tokenCreatedDate = DateTime.Now;
            DateTime tokenExpirationDate = tokenCreatedDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                tokenCreatedDate.ToString(DATE_FORMAT),
                tokenExpirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken);
        }

        private void UpdateUserInfo(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.DaysToExpiry);

            _userRepository.RefreshUserInfo(user);
        }
    }
}
