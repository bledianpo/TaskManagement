using TaskEntity = Domain.Entities.Task;
using Application.DTO;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity?> CreateTaskAsync(CreateTaskRequest task, CancellationToken cancellationToken = default);
        Task<PagedResult<TaskEntity>> GetTasksPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<TaskEntity?> GetTaskByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<TaskEntity?> UpdateTaskAsync(int id, UpdateTaskRequest updatedTask, CancellationToken cancellationToken = default);
        Task<bool> DeleteTaskAsync(int id, CancellationToken cancellationToken = default);
    }
}
