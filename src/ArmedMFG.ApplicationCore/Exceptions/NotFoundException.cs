using System;

namespace ArmedMFG.ApplicationCore.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }
}
