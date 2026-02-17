using Application.Interfaces;
using TaskEntity = Domain.Entities.Task;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskEntity?> CreateTaskAsync(TaskEntity task)
        {
            var createTask = new TaskEntity(task.Title, task.Description, task.UserId, task.Priority, task.Status);
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

        public async Task<TaskEntity?> UpdateTaskAsync(int id, TaskEntity updatedTask)
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

            task.Update(updatedTask.Title, updatedTask.Description, updatedTask.Status, updatedTask.Priority);
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
