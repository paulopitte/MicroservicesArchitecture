
namespace Core.Common.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationAtHours { get; set; }
        public string Issuer { get; set; }
        public IList<string> Issuers { get; set; }
        public string Audience { get; set; }
        public IList<string> Audiences { get; set; }
    }
}
