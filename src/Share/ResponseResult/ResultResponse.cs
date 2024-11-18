namespace Share.ResponseResult;

public abstract class ResultResponse
{
    public bool IsSuccess { get; init; }
    public bool IsFailed => !IsSuccess;
    public string? ErrorMessage { get; set; }
    public string? ServiceCode { get; set; }
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}