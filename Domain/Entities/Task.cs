using Domain.Enums;

namespace Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Enums.TaskStatus Status { get; private set; } = Enums.TaskStatus.Draft;
        public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public Task(string title, string description, int userId, TaskPriority priority = TaskPriority.Medium)
        {
            Title = title;
            Description = description;
            UserId = userId;
            Priority = priority;
            Status = Enums.TaskStatus.Draft;
        }
    }
}
