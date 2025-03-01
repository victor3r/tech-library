using System.Net;

namespace library.exception
{
    public class ConflictException(string message) : LibraryException(message)
    {
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.Conflict;
    }
}
