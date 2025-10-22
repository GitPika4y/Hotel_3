namespace Hotel_3.WPF.Utils;

public class Resource<T>(bool  isSuccess, T? data, string? message = null)
{
    public bool IsSuccess => isSuccess;
    public T? Data => data;
    public string? Message => message;
    
    public static Resource<T> Success(T data) => new(true, data);
    public static Resource<T> Fail(string? message) => new(false, default, message);
}