using Domain.Enums;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public TaskStatus Status { get; private set; } = TaskStatus.Draft;
        public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public Task(string title, string description, int userId, TaskPriority priority = TaskPriority.Medium, TaskStatus status = TaskStatus.Draft)
        {
            Title = title;
            Description = description;
            UserId = userId;
            Priority = priority;
            Status = status;
        }
    }
}
