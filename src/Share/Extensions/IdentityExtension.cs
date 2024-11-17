namespace Share.Extensions;

public class IdentityExtension(IHttpContextAccessor httpContextAccessor)
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;
    
    public string? UserCode()
    {
        try
        {
            return (bool)_user?.Identity!.IsAuthenticated 
                ? _user.FindFirst(ClaimTypes.SerialNumber)?.Value 
                : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Guid UserId()
    {
        try
        {
            if ((bool)_user?.Identity!.IsAuthenticated)
            {
                var result = _user.FindFirst("Id")?.Value ?? string.Empty;
                return Guid.Parse(result);
            }
            else
                return Guid.Empty;
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    }
    public string? UserPhoneNumber()
    {
        try
        {
            if ((bool)_user?.Identity!.IsAuthenticated)
            {
                var sub = _user.FindFirst(ClaimTypes.UserData)?.Value;
                return sub;
            }
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string? Roles()
    {
        if ((bool)_user?.Identity!.IsAuthenticated)
        {
            var claimsIdentity = _user.Identity as ClaimsIdentity;
            return claimsIdentity?.Claims.Where(x => x.Type == ClaimTypes.Role)
                                        .Select(x => x.Value)
                                        .FirstOrDefault();
        }
        else
            return null;
    }

    public string? UserName()
    {
        return _user?.Identity?.Name;
    }
}