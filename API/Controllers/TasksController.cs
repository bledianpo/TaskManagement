using Application.Interfaces;
using TaskEntity = Domain.Entities.Task;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private const int DefaultPageSize = 10;
        private const int MaxPageSize = 100;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageSize < 1 || pageSize > MaxPageSize)
            {
                pageSize = DefaultPageSize;
            }
            var tasks = await _taskService.GetAllTasksAsync(pageNumber, pageSize);
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest createdTask)
        {
            var task = await _taskService.CreateTaskAsync(createdTask);
            if (task == null)
            {
                return BadRequest("Failed to create task");
            }

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest updatedTask)
        {
            var task = await _taskService.UpdateTaskAsync(id, updatedTask);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
