namespace MakeMyPizza.Data.DataExceptions;

public class RecordAlreadyExistsException : Exception
{
    public RecordAlreadyExistsException()
    {
    }

    public RecordAlreadyExistsException(string message)
        : base(message)
    {
    }

    public RecordAlreadyExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}