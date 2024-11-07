namespace UserManagement.Application.ApplicationServices.Roles.Exceptions;

public sealed class RoleAlredyExistException() : MamrpBaseBadRequestException("این نقش از قبل  وجود دارد")
{
    
}