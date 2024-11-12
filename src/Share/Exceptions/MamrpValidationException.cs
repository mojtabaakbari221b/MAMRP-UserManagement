using FluentValidation.Results;

namespace Share.Exceptions;

public sealed class MamrpValidationException : Exception
{
    private MamrpValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public MamrpValidationException(IEnumerable<ValidationFailure> failures, string serviceCode)
        : this()
    {
        ServiceCode = serviceCode;
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
    public string ServiceCode { get; set; } = string.Empty;
}