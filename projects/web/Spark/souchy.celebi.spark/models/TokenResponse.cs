namespace souchy.celebi.spark.models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
        public TokenResponse(string token, DateTime? expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
