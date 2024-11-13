namespace Share.Exceptions;

public abstract class MamrpBaseBadRequestException(string message, string serviceCode) : Exception(message)
{
    public string ServiceCode { get; set; } = serviceCode;
}