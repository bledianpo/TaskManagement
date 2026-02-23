using Application.DTO;
using Application.Validators;
using Xunit;

namespace Application.Tests.Validators;

public class LoginValidatorTests
{
    private readonly LoginValidator _validator = new();

    [Fact]
    public void Valid_login_passes()
    {
        var model = new Login { Email = "user@gmail.com", Password = "password123" };
        var result = _validator.Validate(model);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Empty_email_fails_with_required_message()
    {
        var model = new Login { Email = "", Password = "password123" };
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
        var emailError = result.Errors.FirstOrDefault(e => e.PropertyName == "Email");
        Assert.NotNull(emailError);
        Assert.Equal("Email is required.", emailError.ErrorMessage);
    }

    [Fact]
    public void Short_password_fails_with_minimum_length_message()
    {
        var model = new Login { Email = "user@gmail.com", Password = "12345" };
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
        var passwordError = result.Errors.FirstOrDefault(e => e.PropertyName == "Password");
        Assert.NotNull(passwordError);
        Assert.Equal("Password must be at least 6 characters.", passwordError.ErrorMessage);
    }
}
