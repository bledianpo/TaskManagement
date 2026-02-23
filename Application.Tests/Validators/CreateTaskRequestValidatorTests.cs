using Application.DTO;
using Application.Validators;
using Xunit;

namespace Application.Tests.Validators;

public class CreateTaskRequestValidatorTests
{
    private readonly CreateTaskRequestValidator _validator = new();

    [Fact]
    public void Valid_request_passes()
    {
        var model = new CreateTaskRequest
        {
            Title = "My task",
            Description = "Description",
            Priority = "High",
            Status = "Draft"
        };
        var result = _validator.Validate(model);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Empty_title_fails_with_required_message()
    {
        var model = new CreateTaskRequest
        {
            Title = "",
            Description = "Some description"
        };
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
        var titleError = result.Errors.FirstOrDefault(e => e.PropertyName == "Title");
        Assert.NotNull(titleError);
        Assert.Equal("Title is required!", titleError.ErrorMessage);
    }
}
