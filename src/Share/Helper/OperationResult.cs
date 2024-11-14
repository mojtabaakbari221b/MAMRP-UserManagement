namespace Share.Helper;

// public class OperationResult {
//     protected OperationResult(bool isSuccess = true) {
//         this.Success = isSuccess;
//     }
//     protected OperationResult(string message) {
//         this.Success = false;
//         this.FailureMessage = message;
//     }
//     protected OperationResult(Exception ex) {
//         this.Success = false;
//         this.Exception = ex;
//     }
//     public bool Success {
//         get;
//         protected set;
//     }
//     public string ? FailureMessage {
//         get;
//         protected set;
//     }
//     public Exception ? Exception {
//         get;
//         protected set;
//     }
//
//     public static OperationResult SuccessResult() {
//         return new OperationResult();
//     }
//     public static OperationResult FailureResult(string message) {
//         return new OperationResult(message);
//     }
//     public static OperationResult ExceptionResult(Exception ex) {
//         return new OperationResult(ex);
//     }
//     public bool IsException() {
//         return this.Exception != null;
//     }
//
// }

public class OperationResult<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public List<string> Errors { get; private set; }
    public ErrorType Type { get; set; }

    protected OperationResult(bool isSuccess, T? data, List<string> errors, ErrorType type)
    {
        IsSuccess = isSuccess;
        Data = data;
        Errors = errors ?? [];
        Type = type;
    }

    public static OperationResult<T> Success(T? data = default)
        => new(true, data, [], ErrorType.Success);

    public static OperationResult<T> Failure(List<string> errors, ErrorType type)
        => new(false, default, errors, type);

    public static OperationResult<T> Failure(string error, ErrorType type)
        => new(false, default, [error], type);
}

public sealed class OperationResult : OperationResult<object>
{
    private OperationResult(bool isSuccess, object? data, List<string> errors, ErrorType type)
        : base(isSuccess, data, errors, type)
    {
    }

    public static OperationResult Success()
        => new(true, null, [], ErrorType.Success);

    public new static OperationResult Failure(ErrorType type)
        => new(false, null, [], type);

    public new static OperationResult Failure(List<string> errors, ErrorType type)
        => new(false, null, errors, type);

    public new static OperationResult Failure(string error, ErrorType type)
        => new(false, null, [error], type);
}

public enum ErrorType
{
    Success,
    Errors,
    NotFound,
    Failure
}