using TaskEntity = Domain.Entities.Task;
using Application.DTO;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity?> CreateTaskAsync(CreateTaskRequest task);
        Task<List<TaskEntity>> GetAllTasksAsync(int pageNumber, int pageSize);
        Task<TaskEntity?> GetTaskByIdAsync(int id);
        Task<TaskEntity?> UpdateTaskAsync(int id, UpdateTaskRequest updatedTask);
        Task<bool> DeleteTaskAsync(int id);
    }
}
