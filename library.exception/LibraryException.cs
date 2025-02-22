using System.Net;

namespace library.exception
{
    public abstract class LibraryException : SystemException
    {
        public abstract List<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
