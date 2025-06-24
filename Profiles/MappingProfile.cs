using AutoMapper;
using TaskManager.Models;
using TaskManager.Dtos;

namespace TaskManager.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UsersReadDto>();
            CreateMap<UsersCreateDto, User>();
            CreateMap<UsersUpdateDto, User>();

            // Task -> TaskReadDto
            CreateMap<Tasks, TasksReadDto>()
                .ForMember(dest => dest.AssigneeName, opt => opt.MapFrom(src => src.Assignee.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // TaskCreateDto -> Task
            CreateMap<TasksCreateDto, Tasks>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Models.TaskStatus>(src.Status.ToString())));

            // TaskUpdateDto -> Task
            CreateMap<TasksUpdateDto, Tasks>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Models.TaskStatus>(src.Status.ToString())));
        }
    }
}
