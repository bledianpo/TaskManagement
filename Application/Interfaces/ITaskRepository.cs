using TaskEntity = Domain.Entities.Task;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity task, CancellationToken cancellationToken = default);
        Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<TaskEntity>> GetAllAsync(int pageNumber, int pageSize, TaskStatus? status = null, CancellationToken cancellationToken = default);
        Task<int> GetAllCountAsync(TaskStatus? status = null, CancellationToken cancellationToken = default);
        Task<List<TaskEntity>> GetByUserIdAsync(int userId, int pageNumber, int pageSize, TaskStatus? status = null, CancellationToken cancellationToken = default);
        Task<int> GetCountByUserIdAsync(int userId, TaskStatus? status = null, CancellationToken cancellationToken = default);
        Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default);
        Task DeleteAsync(TaskEntity task, CancellationToken cancellationToken = default);
    }
}
