using System;
using System.Data;

namespace XRisk.Data
{
    public interface IConnectionLocator : IDependency
    {
        IDbConnection For(Type entityType);
    }
}
