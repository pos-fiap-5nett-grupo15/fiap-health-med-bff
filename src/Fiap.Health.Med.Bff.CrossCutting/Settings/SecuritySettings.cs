namespace Fiap.Health.Med.Bff.CrossCutting.Settings
{
    public class SecuritySettings
    {
        public required string JwtSecurityKey { get; set; }
        public int JwtTokenExpiresMinutes { get; set; }
    }
}
