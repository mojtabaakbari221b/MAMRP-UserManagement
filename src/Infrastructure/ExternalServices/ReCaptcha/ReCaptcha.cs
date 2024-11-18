namespace UserManagement.Infrastructure.ExternalServices.ReCaptcha;

public static class ReCaptcha
{
    public static async Task<bool> IsValid(string captchaValue)
    {
        using var client = new HttpClient();
        var secretKey = "6LdowIEqAAAAAHmkM_D1W_pvN9ykOFvMdgI8zbes";
        var verificationUrl = "https://www.google.com/recaptcha/api/siteverify";
            
        var response = await client.PostAsync(
            $"{verificationUrl}?secret={secretKey}&response={captchaValue}",
            null
        );
        
        var result = await response.Content.ReadAsStringAsync();
        return result.Contains("\"success\": true");
    }
}