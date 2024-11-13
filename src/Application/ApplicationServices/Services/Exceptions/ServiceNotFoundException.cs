namespace UserManagement.Application.ApplicationServices.Services.Exceptions;

public class ServiceNotFoundException()
    : MamrpBaseNotFoundException("بخش مورد نظر پیدا نشد.", ServicesCode.UserManagement);