namespace XRisk.Caching
{
    public interface IVolatileToken
    {
        bool IsCurrent { get; }
    }
}