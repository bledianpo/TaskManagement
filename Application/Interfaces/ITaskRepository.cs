using TaskEntity = Domain.Entities.Task;

namespace Application.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity task);
        Task<TaskEntity?> GetByIdAsync(int id);
        Task<List<TaskEntity>> GetAllAsync(int pageNumber, int pageSize);
        Task<int> GetAllCountAsync();
        Task<List<TaskEntity>> GetByUserIdAsync(int userId, int pageNumber, int pageSize);
        Task<int> GetCountByUserIdAsync(int userId);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);
    }
}
