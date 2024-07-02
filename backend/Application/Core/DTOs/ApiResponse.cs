namespace Application.Core.DTOs;

public class ApiResponse<TResult>(bool success,
    string message,
    TResult result,
    object error = null,
    int statusCode = 200)
    where TResult : class
{
    public TResult Result { get; } = result;
    public bool Success { get; } = success;
    public object Error { get; } = error;
    public int StatusCode { get; } = statusCode;
    public string Message { get; } = message;

    public static ApiResponse<TResult> Ok(TResult result, string message = null)
    {
        return new ApiResponse<TResult>(true, message, result);
    }

    public static ApiResponse<TResult> Fail(string message, object error = null, int statusCode = 500,
        TResult result = null)
    {
        return new ApiResponse<TResult>(false, message, result, error, statusCode);
    }
}