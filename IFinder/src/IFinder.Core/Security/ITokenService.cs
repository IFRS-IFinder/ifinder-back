namespace IFinder.Core.Security
{
    public interface ITokenService
    {
        string GenerateToken(string user);
    }
}