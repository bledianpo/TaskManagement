using Domain.Enums;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.DTO
{
    public class UpdateTaskRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskPriority? Priority { get; set; }
        public TaskStatus? Status { get; set; }
    }
}
