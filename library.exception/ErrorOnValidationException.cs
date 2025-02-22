using System.Net;

namespace library.exception
{
    public class ErrorOnValidationException(List<string> errorMessages) : LibraryException
    {

        private readonly List<string> _errors = errorMessages;

        public override List<string> GetErrorMessages() => _errors;

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.BadRequest;
    }
}
