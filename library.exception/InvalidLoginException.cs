using System.Net;

namespace library.exception
{
    public class InvalidLoginException : LibraryException
    {
        public InvalidLoginException() : base("Invalid credentials") { }

        public override List<string> GetErrorMessages() =>
            [Message];

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.Unauthorized;
    }
}
