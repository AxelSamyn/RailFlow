using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.NotificationService.Messaging.Exceptions;

public sealed class NonRetryableException : Exception
{
    public NonRetryableException( string message ) : base( message )
    {
        
    }
}
