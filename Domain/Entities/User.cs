namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public bool IsAdmin { get; private set; }

        public User(string username, string email, string password, bool isAdmin = false)
        {
            Username = username;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }

    }
}
