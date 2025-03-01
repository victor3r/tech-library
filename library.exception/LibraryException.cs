using System.Net;

namespace library.exception
{
    public abstract class LibraryException(string message) : SystemException(message)
    {
        public abstract List<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
