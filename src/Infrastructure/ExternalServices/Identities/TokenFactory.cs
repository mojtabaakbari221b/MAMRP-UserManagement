using UserManagement.Domain.Services.DTOs;

namespace UserManagement.Infrastructure.ExternalServices.Identities;

public sealed class TokenFactory(IOptions<TokenOption> options, UserManager<User> userManager) : ITokenFactory
{
    private readonly BearerTokenOption _optionBearer = options.Value.BearerTokenOption;
    private readonly RefreshTokenOption _optionsRefresh = options.Value.RefreshTokenOption;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<TokenDto> CreateTokenAsync(Guid userId)
    {
        var accessToken = await CreateBearerAccessToken(userId);
        var refreshToken = await CreateRefreshToken(userId);
        return new TokenDto(accessToken, refreshToken);
    }

    private async Task<string> CreateBearerAccessToken(Guid id)
    {
        var claims = new List<Claim>
        {
            // Unique Id for all Jwt tokes
            new(JwtRegisteredClaimNames.Jti, StringUtils.CreateCryptographicallySecureGuid(), ClaimValueTypes.String, _optionBearer.Issuer),
            // Issuer
            new(JwtRegisteredClaimNames.Iss, _optionBearer.Issuer, ClaimValueTypes.String, _optionBearer.Issuer),
            // Issued at
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _optionBearer.Issuer),
            // User Costume Data
            new("Id", id.ToString(), ClaimValueTypes.String, _optionBearer.Issuer),
        };
        // TODO: Add Section code in User token calim
        var user = await _userManager.FindByIdAsync(id.ToString());
        var userClaims = await _userManager.GetClaimsAsync(user!);
        claims.AddRange(userClaims);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_optionBearer.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: _optionBearer.Issuer,
            audience: _optionBearer.Audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(_optionBearer.AccessTokenExpirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> CreateRefreshToken(Guid id)
    {
        var claims = new List<Claim>
        {
            // Unique Id for all Jwt tokes
            new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,
                StringUtils.CreateCryptographicallySecureGuid(), ClaimValueTypes.String, _optionsRefresh.Issuer),
            // Issuer
            new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iss, _optionsRefresh.Issuer,
                ClaimValueTypes.String, _optionsRefresh.Issuer),
            // Issued at
            new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64,
                _optionsRefresh.Issuer),
            // for invalidation
            new(ClaimTypes.SerialNumber, StringUtils.CreateCryptographicallySecureGuid(), ClaimValueTypes.String,
                _optionsRefresh.Issuer),
            // custom data
            new("Id", id.ToString(), ClaimValueTypes.String, _optionsRefresh.Issuer),
            // add roles
            // TODO : اینجا اگه لیست رو به شکل استرینگ برگردونیم ضایع نیستش؟
            // new("Claims", [], ClaimValueTypes.)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_optionsRefresh.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: _optionsRefresh.Issuer,
            audience: _optionsRefresh.Audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(_optionsRefresh.RefreshTokenExpirationMinutes),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}