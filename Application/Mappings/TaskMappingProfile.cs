using Application.DTO;
using AutoMapper;
using Domain.Enums;
using TaskEntity = Domain.Entities.Task;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<CreateTaskRequest, TaskEntity>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => ParsePriority(s.Priority)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => ParseStatus(s.Status)));

            CreateMap<UpdateTaskRequest, TaskEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.Priority, opt => opt.MapFrom((s, d) => s.Priority != null ? ParsePriority(s.Priority) : d.Priority))
                .ForMember(d => d.Status, opt => opt.MapFrom((s, d) => s.Status != null ? ParseStatus(s.Status) : d.Status))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

        private static TaskPriority ParsePriority(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return TaskPriority.Medium;
            return Enum.TryParse<TaskPriority>(value, true, out var p) ? p : TaskPriority.Medium;
        }

        private static TaskStatus ParseStatus(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return TaskStatus.Draft;
            return Enum.TryParse<TaskStatus>(value, true, out var s) ? s : TaskStatus.Draft;
        }
    }
}
