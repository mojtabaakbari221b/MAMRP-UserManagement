namespace UserManagement.Application.ApplicationServices.ServiceGroups.Eceptions;

public class ServiceGroupNotFoundException()
    : MamrpBaseNotFoundException("گروه پیدا نشد.", ServicesCode.UserManagement);