using TaskStatus = Domain.Enums.TaskStatus;
using Domain.Enums;

namespace Application.DTO
{
    public class CreateTaskRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public TaskStatus Status { get; set; } = TaskStatus.Draft;
    }
}
