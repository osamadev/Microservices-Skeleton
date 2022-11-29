namespace Actio.Common.Auth
{
    public class JwtToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}