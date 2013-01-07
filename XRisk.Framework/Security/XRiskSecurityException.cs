using System;
using System.Runtime.Serialization;

namespace XRisk.Security
{
    [Serializable]
    public class OrchardSecurityException : Exception
    {
        public OrchardSecurityException(string message) : base(message) { }
        public OrchardSecurityException(string message, Exception innerException) : base(message, innerException) { }
        protected OrchardSecurityException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public string PermissionName { get; set; }
        public IUser User { get; set; }
    }
}
