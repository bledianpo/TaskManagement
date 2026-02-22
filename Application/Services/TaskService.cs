using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using TaskEntity = Domain.Entities.Task;
using TaskStatus = Domain.Enums.TaskStatus;

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

        public async Task<TaskEntity?> CreateTaskAsync(CreateTaskRequest task, CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var priority = ParsePriority(task.Priority);
            var status = ParseStatus(task.Status) ?? TaskStatus.Draft;
            var createdTask = new TaskEntity(task.Title, task.Description, userId.Value, priority, status);
            await _taskRepository.AddAsync(createdTask, cancellationToken);
            return createdTask;
        }

        private static TaskPriority ParsePriority(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return TaskPriority.Medium;
            return Enum.TryParse<TaskPriority>(value, true, out var p) ? p : TaskPriority.Medium;
        }

        public async Task<PagedResult<TaskEntity>> GetTasksPagedAsync(int pageNumber, int pageSize, string? status = null, CancellationToken cancellationToken = default)
        {
            var statusFilter = ParseStatus(status);

            if (!_currentUserService.IsAuthenticated)
            {
                return BuildPagedResult(new List<TaskEntity>(), 0, pageNumber, pageSize);
            }
            if (_currentUserService.IsAdmin)
            {
                var items = await _taskRepository.GetAllAsync(pageNumber, pageSize, statusFilter, cancellationToken);
                var total = await _taskRepository.GetAllCountAsync(statusFilter, cancellationToken);
                return BuildPagedResult(items, total, pageNumber, pageSize);
            }
            if (_currentUserService.UserId is int userId)
            {
                var items = await _taskRepository.GetByUserIdAsync(userId, pageNumber, pageSize, statusFilter, cancellationToken);
                var total = await _taskRepository.GetCountByUserIdAsync(userId, statusFilter, cancellationToken);
                return BuildPagedResult(items, total, pageNumber, pageSize);
            }
            return BuildPagedResult(new List<TaskEntity>(), 0, pageNumber, pageSize);
        }

        private static TaskStatus? ParseStatus(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return Enum.TryParse<TaskStatus>(value, true, out var s) ? s : null;
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

        public async Task<TaskEntity?> GetTaskByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
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

        public async Task<TaskEntity?> UpdateTaskAsync(int id, UpdateTaskRequest updatedTask, CancellationToken cancellationToken = default)
        {
            if (updatedTask == null)
            {
                throw new ArgumentNullException(nameof(updatedTask));
            }

            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
            if (task == null || (!_currentUserService.IsAdmin && _currentUserService.UserId != task.UserId))
            {
                return null;
            }
            _mapper.Map(updatedTask, task);
            await _taskRepository.UpdateAsync(task, cancellationToken);
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
            if (task == null || (!_currentUserService.IsAdmin && _currentUserService.UserId != task.UserId))
            {
                return false;
            }
            await _taskRepository.DeleteAsync(task, cancellationToken);
            return true;
        }
    }
}
