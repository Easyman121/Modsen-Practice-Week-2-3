using System.Runtime.CompilerServices;

namespace BusinessLogicLayer.Exceptions;

public class NonUniqueException : Exception
{
    public NonUniqueException(string message) : base(message)
    {
    }

    public static void EnsureUnique<T>(
        IList<T> collection,
        Func<T, bool> uniquenessPredicate,
        string errorMsg,
        [CallerArgumentExpression(nameof(collection))]
        string? colName = null,
        [CallerArgumentExpression(nameof(uniquenessPredicate))]
        string? predName = null)
    {
        if (collection == null || collection.Count == 0)
        {
            throw new ArgumentNullException(colName, $"{colName} is empty or null");
        }

        if (collection.Count == 1)
        {
            return;
        }

        if (uniquenessPredicate == null)
        {
            throw new ArgumentNullException(predName, "Predicate expression is null");
        }

        if (collection.Any(uniquenessPredicate))
        {
            throw new NonUniqueException(errorMsg);
        }
    }
}