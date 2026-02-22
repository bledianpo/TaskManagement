using Application.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class GetTasksQueryValidator : AbstractValidator<GetTasksQuery>
    {
        private const int MaxPageSize = 100;
        private static readonly string[] ValidStatuses = { "Draft", "InProgress", "Completed" };

        public GetTasksQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.")
                .When(x => x.PageNumber.HasValue);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, MaxPageSize).WithMessage($"PageSize must be between 1 and {MaxPageSize}.")
                .When(x => x.PageSize.HasValue);

            RuleFor(x => x.Status)
                .Must(v => string.IsNullOrWhiteSpace(v) || ValidStatuses.Contains(v, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Status must be one of: Draft, InProgress, Completed.")
                .When(x => !string.IsNullOrWhiteSpace(x.Status));
        }
    }
}
