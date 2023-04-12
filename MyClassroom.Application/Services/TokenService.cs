using MyClassroom.Core.Models;
using MyClassroom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassroom.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly MyClassroomContext _context;

        public TokenService(MyClassroomContext context)
        {
            _context = context;
        }

        public async Task<Token?> GetTokenById(Guid id)
        {
            var token = await _context.Tokens.FindAsync(id);
            return token;
        }

        public async Task<Token> AddToken(Token token)
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
            return token;
        }
    }
}
