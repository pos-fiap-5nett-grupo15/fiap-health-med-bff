namespace Fiap.Health.Med.Bff.Application.Interfaces.Auth
{
    public interface ITokenHandler
    {
        Task<string> GenerateToken();
    }
}
