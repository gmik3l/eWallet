namespace test.CustomExeptions;

public class UserNotFoundExeption : Exception
{
    public UserNotFoundExeption() { }
    
    public UserNotFoundExeption(string message) : base(message) { }
    
    public UserNotFoundExeption(string message, Exception innerException)
        : base(message, innerException) { }
}




