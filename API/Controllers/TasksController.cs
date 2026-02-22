using Application.Interfaces;
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
        private const int DefaultPageSize = 10;
        private const int MaxPageSize = 100;

        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTasksQuery query, CancellationToken cancellationToken = default)
        {
            var pageNumber = query.PageNumber ?? 1;
            var pageSize = query.PageSize ?? DefaultPageSize;
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > MaxPageSize) pageSize = DefaultPageSize;
            var tasks = await _taskService.GetTasksPagedAsync(pageNumber, pageSize, cancellationToken);
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var task = await _taskService.GetTaskByIdAsync(id, cancellationToken);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest createdTask, CancellationToken cancellationToken = default)
        {
            var task = await _taskService.CreateTaskAsync(createdTask, cancellationToken);
            if (task == null)
            {
                return StatusCode(500, new { message = "Failed to create task. Please try again." });
            }

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest updatedTask, CancellationToken cancellationToken = default)
        {
            var task = await _taskService.UpdateTaskAsync(id, updatedTask, cancellationToken);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _taskService.DeleteTaskAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
