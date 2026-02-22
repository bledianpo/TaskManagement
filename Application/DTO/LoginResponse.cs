namespace Application.DTO
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
