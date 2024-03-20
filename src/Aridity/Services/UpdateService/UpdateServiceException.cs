using System;
using System.Runtime.Serialization;

namespace Aridity.Services
{
    [Serializable()]
    public class UpdateServiceException : Exception
    {
        public UpdateServiceException() : base("An exception occured while updating.") { }
        public UpdateServiceException(string message) : base("An exception occured while updating: " + message) { }
        public UpdateServiceException(string message, Exception innerException) : base(
            "An exception occured while updating: " + message, innerException)
        { }
    }
}
