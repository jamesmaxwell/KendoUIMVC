using System;
using System.Runtime.Serialization;

namespace XRisk
{
    [Serializable]
    public class XRiskFatalException : Exception
    {

        public XRiskFatalException(string message)
            : base(message)
        {
        }

        public XRiskFatalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected XRiskFatalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
