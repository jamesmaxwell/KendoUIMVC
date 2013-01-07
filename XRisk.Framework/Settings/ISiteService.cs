namespace XRisk.Settings
{
    public interface ISiteService : IDependency
    {
        ISite GetSiteSettings();
    }
}
