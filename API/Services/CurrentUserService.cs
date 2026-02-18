using System.Security.Claims;
using Application.Interfaces;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var sub = user?.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.TryParse(sub, out var id) ? id : null;
            }
        }

        public bool IsAdmin =>
            string.Equals(_httpContextAccessor.HttpContext?.User?.FindFirstValue("isAdmin"), "true", StringComparison.OrdinalIgnoreCase);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
