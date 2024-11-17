namespace Share.Extensions;

public static class IdentityExtension
{
    public static string? UserCode(this ClaimsPrincipal user)
    {
        try
        {
            return user.Identity!.IsAuthenticated 
                ? user.FindFirst(ClaimTypes.SerialNumber)?.Value 
                : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static Guid UserId(this ClaimsPrincipal user)
    {
        try
        {
            if (user.Identity!.IsAuthenticated)
            {
                var result = user.FindFirst("Id")?.Value ?? string.Empty;
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
    public static string? UserPhoneNumber(this ClaimsPrincipal user)
    {
        try
        {
            if (user.Identity!.IsAuthenticated)
            {
                var sub = user.FindFirst(ClaimTypes.UserData)?.Value;
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

    public static string? Roles(this ClaimsPrincipal user)
    {
        if (user.Identity!.IsAuthenticated)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity?.Claims.Where(x => x.Type == ClaimTypes.Role)
                                        .Select(x => x.Value)
                                        .FirstOrDefault();
        }
        else
            return null;
    }

    public static string? UserName(this ClaimsPrincipal user)
    {
        return user.Identity?.Name;
    }
}