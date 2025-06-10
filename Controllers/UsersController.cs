using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using TaskManager.Models;
using TaskManager.Dtos;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/[user]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<UsersReadDto>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            var usersReadDto = _mapper.Map<IEnumerable<UsersReadDto>>(users);
            return Ok(usersReadDto);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<UsersReadDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            var userReadDto = _mapper.Map<UsersReadDto>(user);
            return Ok(userReadDto);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<UsersReadDto> CreateUser(UsersCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            _userService.CreateUser(userModel);
            _userService.SaveChanges();

            var userReadDto = _mapper.Map<UsersReadDto>(userModel);

            return CreatedAtAction(nameof(GetUserById), new { id = userModel.Id }, userReadDto);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UsersUpdateDto userUpdateDto)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            _mapper.Map(userUpdateDto, existingUser);
            _userService.UpdateUser(existingUser);
            _userService.SaveChanges();

            return NoContent();
        }

        // PATCH: api/users/{id}
        [HttpPatch("{id}")]
        public ActionResult PatchUser(int id, JsonPatchDocument<UsersUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            var userToPatch = _mapper.Map<UsersUpdateDto>(existingUser);
            patchDoc.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(userToPatch, existingUser);
            _userService.UpdateUser(existingUser);
            _userService.SaveChanges();

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            _userService.DeleteUser(id);
            _userService.SaveChanges();

            return NoContent();
        }
    }
}
