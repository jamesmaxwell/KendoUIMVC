using System;
using System.Web;

namespace XRisk.Mvc
{
    public interface IHttpContextAccessor
    {
        HttpContextBase Current();
    }

    public class HttpContextAccessor : IHttpContextAccessor
    {
        public HttpContextBase Current()
        {
            var httpContext = GetStaticProperty();
            if (httpContext == null)
                return null;
            return new HttpContextWrapper(httpContext);
        }

        private HttpContext GetStaticProperty()
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return null;
            }
            return httpContext;
        }
    }
}
