using System.Net;

namespace library.exception
{
    public class NotFoundException(string message) : LibraryException(message)
    {
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
