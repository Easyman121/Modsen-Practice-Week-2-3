namespace BusinessLogicLayer.Exceptions;

internal class RequestDtoException : Exception
{
    public RequestDtoException(string message) : base(message)
    {
    }
}