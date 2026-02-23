using Application.Interfaces;
using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Seed;

public static class AdminSeeder
{
    public static async Task SeedAdminIfNoneAsync(
        IUserRepository repo,
        IPasswordHasher hasher,
        string email,
        string password,
        string username,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var existing = await repo.GetByEmailAsync(normalizedEmail, cancellationToken);
        if (existing == null)
        {
            var user = new User(
            username.Trim(),
            normalizedEmail,
            hasher.Hash(password),
            isAdmin: true);

            await repo.AddAsync(user, cancellationToken);
        }
    }
}
