using TaskEntity = Domain.Entities.Task;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity?> CreateTaskAsync(TaskEntity task);
        Task<List<TaskEntity>> GetAllTasksAsync(int pageNumber, int pageSize);
        Task<TaskEntity?> GetTaskByIdAsync(int id);
        Task<TaskEntity?> UpdateTaskAsync(int id, TaskEntity updatedTask);
        Task<bool> DeleteTaskAsync(int id);
    }
}
