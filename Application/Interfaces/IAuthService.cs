using Application.DTO;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(Register dto);
        Task<LoginResponse?> LoginAsync(Login dto);
    }
}
