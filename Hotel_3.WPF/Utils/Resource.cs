using System.Text;

namespace Hotel_3.WPF.Utils;

public class Resource<T>(bool  isSuccess, T? data, string? message = null, Exception? exception = null)
{
    public bool IsSuccess => isSuccess;
    public T? Data => data;
    public string? Message => message;
    public Exception? Exception => exception;

    public string GetExceptionDetails()
    {
        if (Message == null)
            return Message ?? "No error details available";
            
        var sb = new StringBuilder();
        var current = Exception;
        while (current != null)
        {
            sb.AppendLine($"{current.GetType().Name}: {current.Message}");
            current = current.InnerException;
        }
        return sb.ToString();
    }
    
    public static Resource<T> Success(T data) => new(true, data);
    public static Resource<T> Fail(string? message) => new(false, default, message);
    public static Resource<T> Fail(Exception ex) => new(false, default, ex.Message, ex);
    public static Resource<T> Fail(string? message, Exception ex) => new(false, default, message, ex);
}