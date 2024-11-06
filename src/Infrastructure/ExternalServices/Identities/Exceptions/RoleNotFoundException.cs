using Share.Exceptions;

namespace UserManagement.Infrastructure.ExternalServices.Identities.Exceptions;

public class RoleNotFoundException() : MamrpBaseNotFoundException("role not found");