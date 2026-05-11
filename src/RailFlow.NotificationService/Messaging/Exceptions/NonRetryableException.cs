namespace RailFlow.NotificationService.Messaging.Exceptions;

public sealed class NonRetryableException : Exception
{
    public NonRetryableException( string message ) : base( message )
    {

    }
    public NonRetryableException(
        string message,
        Exception innerException )
        : base( message, innerException )
    {
    }
}
