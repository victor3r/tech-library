using System.Net;

namespace library.exception
{
    public class InvalidLoginException : LibraryException
    {
        public override List<string> GetErrorMessages() =>
            ["Invalid credentials"];

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.Unauthorized;
    }
}
