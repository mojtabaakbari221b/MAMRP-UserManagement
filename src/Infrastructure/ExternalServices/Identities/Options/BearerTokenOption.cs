namespace UserManagement.Infrastructure.ExternalServices.Identities.Options;

public class TokenOption
{
    public BearerTokenOption BearerTokenOption { get; set; } = null!;
    public RefreshTokenOption RefreshTokenOption { get; set; } = null!;
}


public sealed class BearerTokenOption
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int AccessTokenExpirationMinutes { get; set; }
}

public sealed class RefreshTokenOption
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int RefreshTokenExpirationMinutes { get; set; }
}