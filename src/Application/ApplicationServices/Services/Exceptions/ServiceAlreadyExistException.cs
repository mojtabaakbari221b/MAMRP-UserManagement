namespace UserManagement.Application.ApplicationServices.Services.Exceptions;

public class ServiceAlreadyExistException() : MamrpBaseNotFoundException("سرویس با این کد وجود دارد.", ServicesCode.UserManagement);