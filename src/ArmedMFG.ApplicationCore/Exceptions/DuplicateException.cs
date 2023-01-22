using System;

namespace ArmedMFG.ApplicationCore.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException(string message) : base(message)
    {

    }

}
