using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Dtos;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TasksReadDto>> GetAllTasks()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    return BadRequest("Invalid token claims - missing user ID");

                if (string.IsNullOrEmpty(userRoleClaim))
                    return BadRequest("Invalid token claims - missing user role");

                if (!int.TryParse(userIdClaim, out int userId))
                    return BadRequest("Invalid token claims - user ID not valid");

                bool isAdmin = userRoleClaim.Equals("admin", StringComparison.OrdinalIgnoreCase);

                var tasks = isAdmin
                    ? _taskService.GetAllTasks()
                    : _taskService.GetTasksByUserId(userId);

                return Ok(_mapper.Map<IEnumerable<TasksReadDto>>(tasks));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<TasksReadDto> CreateTask(TasksCreateDto taskCreateDto)
        {
            var taskModel = _mapper.Map<Tasks>(taskCreateDto);
            _taskService.CreateTask(taskModel);

            var taskReadDto = _mapper.Map<TasksReadDto>(taskModel);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskReadDto.Id }, taskReadDto);
        }

        [HttpGet("{id}")]
        public ActionResult<TasksReadDto> GetTaskById(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null) return NotFound();

            return Ok(_mapper.Map<TasksReadDto>(task));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TasksUpdateDto taskUpdateDto)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null) return NotFound();

            _mapper.Map(taskUpdateDto, task);
            _taskService.UpdateTask(task);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask(int id, JsonPatchDocument<TasksUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var task = _taskService.GetTaskById(id);
            if (task == null) return NotFound();

            var taskToPatch = _mapper.Map<TasksUpdateDto>(task);
            patchDoc.ApplyTo(taskToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(taskToPatch, task);
            _taskService.UpdateTask(task);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null) return NotFound();

            _taskService.DeleteTask(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "admin")] // Only admin should be able to query other users' tasks
        public ActionResult<IEnumerable<TasksReadDto>> GetTasksByUserId(int userId)
        {
            var tasks = _taskService.GetTasksByUserId(userId);
            return Ok(_mapper.Map<IEnumerable<TasksReadDto>>(tasks));
        }

        [HttpGet("status/{status}")]
        public ActionResult<IEnumerable<TasksReadDto>> GetTasksByStatus(Models.TaskStatus status)
        {
            var tasks = _taskService.GetTasksByStatus(status);
            return Ok(_mapper.Map<IEnumerable<TasksReadDto>>(tasks));
        }

        [HttpGet("created/{date}")]
        public ActionResult<IEnumerable<TasksReadDto>> GetTasksByDateCreated(DateTime date)
        {
            var tasks = _taskService.GetTasksByDateCreated(date);
            return Ok(_mapper.Map<IEnumerable<TasksReadDto>>(tasks));
        }

        [HttpGet("handedin/{date}")]
        public ActionResult<IEnumerable<TasksReadDto>> GetTasksByDateHandedIn(DateTime date)
        {
            var tasks = _taskService.GetTasksByDateHandedIn(date);
            return Ok(_mapper.Map<IEnumerable<TasksReadDto>>(tasks));
        }
    }
}
