using Application.DTO;
using AutoMapper;
using TaskEntity = Domain.Entities.Task;

namespace Application.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<CreateTaskRequest, TaskEntity>();

            CreateMap<UpdateTaskRequest, TaskEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
