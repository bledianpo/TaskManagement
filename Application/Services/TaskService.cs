using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using TaskEntity = Domain.Entities.Task;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public TaskService(ITaskRepository taskRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TaskEntity?> CreateTaskAsync(CreateTaskRequest task)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
            {
                return null;
            }
            var createdTask = _mapper.Map<TaskEntity>(task);
            createdTask.UserId = userId.Value;
            await _taskRepository.AddAsync(createdTask);
            return createdTask;
        }

        public async Task<PagedResult<TaskEntity>> GetTasksPagedAsync(int pageNumber, int pageSize)
        {
            if (!_currentUserService.IsAuthenticated)
            {
                return BuildPagedResult(new List<TaskEntity>(), 0, pageNumber, pageSize);
            }
            else if (_currentUserService.IsAdmin)
            {
                var items = await _taskRepository.GetAllAsync(pageNumber, pageSize);
                var total = await _taskRepository.GetAllCountAsync();
                return BuildPagedResult(items, total, pageNumber, pageSize);
            }
            else if (_currentUserService.UserId is int userId)
            {
                var items = await _taskRepository.GetByUserIdAsync(userId, pageNumber, pageSize);
                var total = await _taskRepository.GetCountByUserIdAsync(userId);
                return BuildPagedResult(items, total, pageNumber, pageSize);
            }
            return BuildPagedResult(new List<TaskEntity>(), 0, pageNumber, pageSize);
        }

        private static PagedResult<TaskEntity> BuildPagedResult(List<TaskEntity> items, int totalCount, int pageNumber, int pageSize)
        {
            var totalPages = pageSize > 0 ? (int)Math.Ceiling(totalCount / (double)pageSize) : 0;
            return new PagedResult<TaskEntity>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<TaskEntity?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return null;
            }
            else if (_currentUserService.IsAdmin)
            {
                return task;
            }
            else if (_currentUserService.UserId == task.UserId)
            {
                return task;
            }

            return null;
        }

        public async Task<TaskEntity?> UpdateTaskAsync(int id, UpdateTaskRequest updatedTask)
        {
            if (updatedTask == null)
            {
                throw new ArgumentNullException(nameof(updatedTask));
            }

            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null || (!_currentUserService.IsAdmin && _currentUserService.UserId != task.UserId))
            {
                return null;
            }
            _mapper.Map(updatedTask, task);
            await _taskRepository.UpdateAsync(task);
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null || (!_currentUserService.IsAdmin && _currentUserService.UserId != task.UserId))
            {
                return false;
            }
            await _taskRepository.DeleteAsync(task);
            return true;
        }
    }
}
