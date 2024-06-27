namespace BusinessLogicLayer.Exceptions;

/// <summary>
/// Exception type for sanity check
/// </summary>
public sealed class TestCustomException() : Exception("Came, saw, threw")
{
    public override Dictionary<string, int> Data { get; } = new() { ["banana"] = 5, ["user"] = 10 };
}

// Expected result from GlobalExceptionHandler for this type is:
// {
//   "title": "Unavailable For Legal Reasons",
//   "status": 451,
//   "detail": "Came, saw, threw",
//   "traceId": "0HN4LSER3QEL2:00000017", <!--number will be different-->
//   "data": {
//     "Banana": 5,
//     "User": 10
//   }
// }

// Expected result from GlobalExceptionHandler in Release (Production Environment) for this type is:
// {
//   "title": "Unavailable For Legal Reasons",
//   "status": 451,
//   "detail": "Came, saw, threw"
// }