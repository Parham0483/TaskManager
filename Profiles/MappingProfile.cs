using AutoMapper;
using TaskManager.Models;
using TaskManager.Dtos;

namespace TaskManager.Profiles
{
    public class MappingProfile : Profile
    {
        public  MappingProfile()
        {
            // User mappings
            CreateMap<User, UsersReadDto>();
            CreateMap<UsersCreateDto, User>();
            CreateMap<UsersUpdateDto, User>();

            // Task mappings
            CreateMap<Tasks, TasksReadDto>();
            CreateMap<TasksCreateDto, Tasks>();
            CreateMap<TasksUpdateDto, Tasks>();
        }
    }
}