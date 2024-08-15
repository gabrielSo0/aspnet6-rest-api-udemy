namespace RestAPI.Data.VO
{
    public class TokenVO
    {
        public TokenVO(bool isAuthenticated, string created, string expiration, string accessToken, string refreshToken)
        {
            IsAuthenticated = isAuthenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public bool IsAuthenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
