using MyClassroom.Core.Models;

namespace MyClassroom.Application.Services
{
    public interface ITokenService
    {
        Task<Token> AddToken(Token token);
        Task<Token?> GetTokenById(Guid id);
    }
}