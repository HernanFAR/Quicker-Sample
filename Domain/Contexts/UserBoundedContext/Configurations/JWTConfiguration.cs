namespace Domain.Contexts.UserBoundedContext.Configurations
{
    public class JWTConfiguration
    {
        public string IssuerSigningKey { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public double Duration { get; set; }
    }
}
