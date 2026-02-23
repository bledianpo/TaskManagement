using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<Result> RegisterAsync(Register dto, CancellationToken cancellationToken = default)
        {
            var email = dto.Email?.Trim().ToLowerInvariant() ?? "";
            var existUser = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (existUser != null)
            {
                return Result.Fail("Email already registered");
            }

            var user = new User(
                dto.Username?.Trim() ?? "",
                email,
                _passwordHasher.Hash(dto.Password?.Trim() ?? ""),
                isAdmin: false);

            await _userRepository.AddAsync(user, cancellationToken);

            return Result.Ok();
        }

        public async Task<LoginResponse?> LoginAsync(Login dto, CancellationToken cancellationToken = default)
        {
            var email = dto.Email?.Trim().ToLowerInvariant() ?? "";
            var password = dto.Password ?? "";
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (user == null || !_passwordHasher.Verify(password, user.Password))
            {
                return null;
            }

            var token = _jwtService.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
