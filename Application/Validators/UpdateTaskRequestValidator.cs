using Application.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
    {
        private static readonly string[] ValidPriorities = { "Low", "Medium", "High" };
        private static readonly string[] ValidStatuses = { "Draft", "InProgress", "Completed" };

        public UpdateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.Title));
            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
            RuleFor(x => x.Priority)
                .Must(v => string.IsNullOrWhiteSpace(v) || ValidPriorities.Contains(v, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Priority is not valid. Allowed values: Low, Medium, High.")
                .When(x => !string.IsNullOrEmpty(x.Priority));
            RuleFor(x => x.Status)
                .Must(v => string.IsNullOrWhiteSpace(v) || ValidStatuses.Contains(v, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Status is not valid. Allowed values: Draft, InProgress, Completed.")
                .When(x => !string.IsNullOrEmpty(x.Status));
        }
    }
}
