using Application.DTO;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(Register dto, CancellationToken cancellationToken = default);
        Task<LoginResponse?> LoginAsync(Login dto, CancellationToken cancellationToken = default);
    }
}
