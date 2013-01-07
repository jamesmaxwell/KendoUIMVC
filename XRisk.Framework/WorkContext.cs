using System;
using System.Web;
using XRisk.Security;

namespace XRisk
{
    /// <summary>
    /// A work context for work context scope
    /// </summary>
    public abstract class WorkContext
    {
        /// <summary>
        /// Resolves a registered dependency type
        /// </summary>
        /// <typeparam name="T">The type of the dependency</typeparam>
        /// <returns>An instance of the dependency if it could be resolved</returns>
        public abstract T Resolve<T>();

        public abstract T GetState<T>(string name);
        public abstract void SetState<T>(string name, T value);


        /// <summary>
        /// The http context corresponding to the work context
        /// </summary>
        public HttpContextBase HttpContext
        {
            get { return GetState<HttpContextBase>("HttpContext"); }
            set { SetState("HttpContext", value); }
        }

        /// <summary>
        /// The user, if there is any corresponding to the work context
        /// </summary>
        public IUser CurrentUser
        {
            get { return GetState<IUser>("CurrentUser"); }
            set { SetState("CurrentUser", value); }
        }
    }
}
