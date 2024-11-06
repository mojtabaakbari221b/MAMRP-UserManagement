using Share.Exceptions;

namespace UserManagement.Infrastructure.ExternalServices.Identities.Exceptions;

public class UserNotFoundException() : MamrpBaseNotFoundException("user not found");