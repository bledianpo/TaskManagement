using Application.Interfaces;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TaskEntity = Domain.Entities.Task;
using Task = System.Threading.Tasks.Task;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TaskEntity>> GetAllAsync(int pageNumber, int pageSize, TaskStatus? status = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Tasks.AsNoTracking();
            if (status.HasValue) {
                query = query.Where(t => t.Status == status.Value);
            }
            return await query
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetAllCountAsync(TaskStatus? status = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Tasks.AsQueryable();
            if (status.HasValue) {
                query = query.Where(t => t.Status == status.Value);
            }
            return await query.CountAsync(cancellationToken);
        }

        public async Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<List<TaskEntity>> GetByUserIdAsync(int userId, int pageNumber, int pageSize, TaskStatus? status = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Tasks.AsNoTracking().Where(t => t.UserId == userId);
            if (status.HasValue) {
                query = query.Where(t => t.Status == status.Value);
            }
            return await query
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetCountByUserIdAsync(int userId, TaskStatus? status = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Tasks.Where(t => t.UserId == userId);
            if (status.HasValue) {
                query = query.Where(t => t.Status == status.Value);
            }
            return await query.CountAsync(cancellationToken);
        }

        public async Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
