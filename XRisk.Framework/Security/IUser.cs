namespace XRisk.Security
{
    /// <summary>
    /// Interface provided by the "User" model. 
    /// </summary>
    public interface IUser
    {
        long Id { get; }
        string UserNo { get; }
        string Email { get; }
    }
}
