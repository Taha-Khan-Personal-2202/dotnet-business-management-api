namespace DotNetBusinessWorkFlow.Application.DTOs.Common;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty; 
    public int StatusCode { get; set; }
    public T? Data { get; set; }

    public static OperationResult<T> Succces(T? data, string message = "Request completed successfully.", int statusCode = 200)
    {
        return new OperationResult<T>()
        {
            Success = true,
            Message = message,
            StatusCode = statusCode,
            Data = data
        };
    }

    public static OperationResult<T> Fail(string message, int statusCode = 400, T? data = default)
    {
        return new OperationResult<T>
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Data = data
        };
    }
}
