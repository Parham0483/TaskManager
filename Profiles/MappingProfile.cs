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

            // Task -> TasksReadDto
        CreateMap<Tasks, TasksReadDto>()
            .ForMember(dest => dest.AssigneeName, opt => opt.MapFrom(src => src.Assignee.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // TasksCreateDto -> Tasks (string -> enum)
        CreateMap<TasksCreateDto, Tasks>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Models.TaskStatus>(src.Status, true)));

        // TasksUpdateDto -> Tasks (string -> enum)
        CreateMap<TasksUpdateDto, Tasks>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Models.TaskStatus>(src.Status, true)));
        }
    }
}
