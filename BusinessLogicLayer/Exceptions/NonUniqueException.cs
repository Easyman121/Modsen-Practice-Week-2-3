namespace BusinessLogicLayer.Exceptions;

internal class NonUniqueException : Exception

{
    public NonUniqueException(string message) : base(message)
    {
    }
}