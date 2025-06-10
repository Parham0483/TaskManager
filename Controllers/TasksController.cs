using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using TaskManager.Models;
using TaskManager.Dtos;
using TaskManager.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskManager.Controllers
{
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
            var tasks = _taskService.GetAllTasks();
            return Ok(_mapper.Map<IEnumerable<TasksReadDto>>(tasks));
        }

        [HttpGet("{id}")]
        public ActionResult<TasksReadDto> GetTaskById(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();

            return Ok(_mapper.Map<TasksReadDto>(task));
        }

        [HttpPost]
        public ActionResult<TasksReadDto> CreateTask(TasksCreateDto taskCreateDto)
        {
            var taskModel = _mapper.Map<Tasks>(taskCreateDto);
            _taskService.CreateTask(taskModel);

            var taskReadDto = _mapper.Map<TasksReadDto>(taskModel);

            return CreatedAtAction(nameof(GetTaskById), new { id = taskReadDto.Id }, taskReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, TasksUpdateDto taskUpdateDto)
        {
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
                return NotFound();

            _mapper.Map(taskUpdateDto, existingTask);

            _taskService.UpdateTask(existingTask);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatchTask(int id, JsonPatchDocument<TasksUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
                return NotFound();

            var taskToPatch = _mapper.Map<TasksUpdateDto>(existingTask);
            patchDoc.ApplyTo(taskToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(taskToPatch, existingTask);

            _taskService.UpdateTask(existingTask);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
                return NotFound();

            _taskService.DeleteTask(id);

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<TasksReadDto>> GetTasksByUserId(int userId)
        {
            var tasks = _taskService.GetTasksByUserId(userId);
            return Ok(_mapper.Map<IEnumerable<TasksReadDto>>(tasks));
        }

        [HttpGet("status/{status}")]
        public ActionResult<IEnumerable<TasksReadDto>> GetTasksByStatus(TaskManager.Models.TaskStatus status)
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
