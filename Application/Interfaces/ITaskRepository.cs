using TaskEntity = Domain.Entities.Task;

namespace Application.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity task);
        Task<TaskEntity?> GetByIdAsync(int id);
        Task<List<TaskEntity>> GetAllAsync();
        Task<List<TaskEntity>> GetByUserIdAsync(int userId);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);
    }
}
