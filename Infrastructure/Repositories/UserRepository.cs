using Domain.Entities;
using Infrastructure.Data;
using Task = System.Threading.Tasks.Task;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            var normalized = email.Trim().ToLowerInvariant();
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalized, cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
