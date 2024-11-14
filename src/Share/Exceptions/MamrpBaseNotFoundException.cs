namespace Share.Exceptions;

public abstract class MamrpBaseNotFoundException(string message, string serviceCode) : Exception(message)
{
    public string ServiceCode { get; set; } = serviceCode;
    private List<string>? Errors { get; set; } = null;
}