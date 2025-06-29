using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using TaskManager.Models;
using TaskManager.Dtos;
using TaskManager.Services;
using System;

namespace TaskManager.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<UsersReadDto>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            var usersDto = _mapper.Map<IEnumerable<UsersReadDto>>(users);
            return Ok(usersDto);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<UsersReadDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            var userDto = _mapper.Map<UsersReadDto>(user);
            return Ok(userDto);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<UsersReadDto> CreateUser(UsersCreateDto userCreateDto)
        {

            var user = _mapper.Map<User>(userCreateDto);
            _userService.CreateUser(user);
            _userService.SaveChanges();

            var userDto = _mapper.Map<UsersReadDto>(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UsersUpdateDto userUpdateDto)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null) return NotFound();

            _mapper.Map(userUpdateDto, existingUser);
            _userService.UpdateUser(existingUser);
            _userService.SaveChanges();

            return NoContent();
        }

        // PATCH: api/users/{id}
        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, JsonPatchDocument<UsersUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var existingUser = _userService.GetUserById(id);
            if (existingUser == null) return NotFound();

            var userToPatch = _mapper.Map<UsersUpdateDto>(existingUser);
            patchDoc.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(userToPatch, existingUser);
            _userService.UpdateUser(existingUser);
            _userService.SaveChanges();

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            _userService.DeleteUser(id);
            _userService.SaveChanges();

            return NoContent();
        }


        // POST: api/users/login
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.PhoneNo) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest(new { message = "Phone number and password must be provided." });
            }

            var user = _userService.Login(loginDto.PhoneNo, loginDto.Password);
            if (user == null)
            {
                // Either user not found or password mismatch
                return Unauthorized(new { message = "Invalid phone number or password." });
            }

            var token = _tokenService.CreateToken(user);

            return Ok(new
            {
                token,
                userId = user.Id,
                userName = user.Name,
                expiration = DateTime.UtcNow.AddHours(3),
            });
        }
    }
}
