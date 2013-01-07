using XRisk.ContentManagement;
using XRisk.Security.Permissions;

namespace XRisk.Security
{
    /// <summary>
    /// Entry-point for configured authorization scheme. Role-based system
    /// provided by default. 
    /// </summary>
    public interface IAuthorizationService : IDependency
    {
        void CheckAccess(Permission permission, IUser user, IContent content);
        bool TryCheckAccess(Permission permission, IUser user, IContent content);
    }

    public class RolesBasedAuthorizationService : IAuthorizationService
    {

        public void CheckAccess(Permission permission, IUser user, IContent content)
        {
            throw new System.NotImplementedException();
        }

        public bool TryCheckAccess(Permission permission, IUser user, IContent content)
        {
            throw new System.NotImplementedException();
        }
    }
}