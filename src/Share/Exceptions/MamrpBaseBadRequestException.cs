namespace Share.Exceptions;

public abstract class MamrpBaseBadRequestException : Exception
{
    protected MamrpBaseBadRequestException(string message, string serviceCode) : base(message)
    {
        ServiceCode = serviceCode;
        Errors = [];
    }

    protected MamrpBaseBadRequestException(List<string> errors, string serviceCode)
    {
        ServiceCode = serviceCode;
        Errors = errors;
    }
    public string ServiceCode { get; set; }
    public List<string> Errors { get; set; }
    
}