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

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskEntity?> CreateTaskAsync(CreateTaskRequest task)
        {
            var createTask = _mapper.Map<TaskEntity>(task);
            await _taskRepository.AddAsync(createTask);
            return createTask;
        }

        public async Task<List<TaskEntity>> GetAllTasksAsync(int pageNumber, int pageSize)
        {
            return await _taskRepository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<TaskEntity?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return null;
            }

            return task;
        }

        public async Task<TaskEntity?> UpdateTaskAsync(int id, UpdateTaskRequest updatedTask)
        {
            if (updatedTask == null)
            {
                throw new ArgumentNullException(nameof(updatedTask));
            }

            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
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
            if (task == null) return false;
            await _taskRepository.DeleteAsync(task);
            return true;
        }
    }
}
