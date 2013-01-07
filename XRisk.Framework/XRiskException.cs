using System;
using System.Runtime.Serialization;

namespace XRisk
{
    [Serializable]
    public class XRiskException : ApplicationException
    {
        public XRiskException(string message)
            : base(message)
        {
        }

        public XRiskException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected XRiskException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}