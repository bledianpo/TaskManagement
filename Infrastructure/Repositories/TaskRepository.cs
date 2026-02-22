using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TaskEntity = Domain.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskEntity>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Tasks
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetAllCountAsync()
        {
            return await _context.Tasks.CountAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskEntity>> GetByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountByUserIdAsync(int userId)
        {
            return await _context.Tasks.CountAsync(t => t.UserId == userId);
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskEntity task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
