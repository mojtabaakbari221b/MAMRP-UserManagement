using Share.Exceptions;

namespace UserManagement.Infrastructure.ExternalServices.ReCaptcha.Exceptions;


public class ReCaptchaFailedException()
    : MamrpBaseBadRequestException("ری‌کپچا به درستی وارد نشده است.", ServicesCode.UserManagement);
    