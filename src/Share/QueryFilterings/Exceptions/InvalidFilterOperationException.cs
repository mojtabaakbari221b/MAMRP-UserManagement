namespace Share.QueryFilterings.Exceptions;

public sealed class InvalidFilterOperationException() : MamrpBaseBadRequestException("این عملگر معتبر نیست.", ServicesCode.UserManagement);