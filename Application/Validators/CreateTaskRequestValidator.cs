using Application.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        private static readonly string[] ValidPriorities = { "Low", "Medium", "High" };
        private static readonly string[] ValidStatuses = { "Draft", "InProgress", "Completed" };

        public CreateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required!")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required!")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");
            RuleFor(x => x.Priority)
                .Must(v => string.IsNullOrWhiteSpace(v) || ValidPriorities.Contains(v, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Priority is not valid. Allowed values: Low, Medium, High.");
            RuleFor(x => x.Status)
                .Must(v => string.IsNullOrWhiteSpace(v) || ValidStatuses.Contains(v, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Status is not valid. Allowed values: Draft, InProgress, Completed.");
        }
    }
}
