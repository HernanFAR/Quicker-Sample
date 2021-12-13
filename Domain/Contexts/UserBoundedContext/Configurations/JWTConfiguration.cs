namespace Domain.Contexts.UserBoundedContext.Configurations
{
    public class JWTConfiguration
    {
        public JWTConfiguration(string issuerSigningKey, string issuer, string audience, double duration)
        {
            IssuerSigningKey = issuerSigningKey;
            Issuer = issuer;
            Audience = audience;
            Duration = duration;
        }

        public string IssuerSigningKey { get; }

        public string Issuer { get; }

        public string Audience { get; }

        public double Duration { get; }
    }
}
