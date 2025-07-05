using AutoMapper;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Domain.Entities;
using Task = ProjectManagement.Domain.Entities.Task;

namespace ProjectManagement.Application.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(destination => destination.ID, option => option.MapFrom(src => src.ProjectID))
                .ForMember(destination => destination.CreatedByUserName, option => option.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.Username : string.Empty))
                .ReverseMap();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();

            CreateMap<Task, TaskDto>()
            .ForMember(destination => destination.ID, option => option.MapFrom(src => src.TaskID))
                .ForMember(destination => destination.ProjectID, option => option.MapFrom(src => src.ProjectID))
                .ForMember(destination => destination.ProjectName, option => option.MapFrom(src => src.Project != null ? src.Project.ProjectName : string.Empty))
                .ForMember(destination => destination.AssignedToUserID, option => option.MapFrom(src => src.AssignedToUserID))
                .ForMember(destination => destination.AssignedToUserName, option => option.MapFrom(src => src.AssignedToUser != null ? src.AssignedToUser.Username : string.Empty))
                .ReverseMap();
            CreateMap<CreateTaskDto, Task>();
            CreateMap<UpdateTaskDto, Task>();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>()
                .ForMember(destionation => destionation.PasswordHash, option => option.MapFrom(source => source.Password));
            CreateMap<UpdateUserDto, User>();

            CreateMap<Comment, CommentDto>()
                .ForMember(destination => destination.ID, option => option.MapFrom(src => src.CommentID))
                .ForMember(destination => destination.TaskID, option => option.MapFrom(src => src.TaskID))
                .ForMember(destination => destination.ProjectID, option => option.MapFrom(src => src.ProjectID))
                .ForMember(destination => destination.UserName, option => option.MapFrom(source => source.User != null ? source.User.Username : string.Empty));
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();

            CreateMap<Attachment, AttachmentDto>()
                 .ForMember(destination => destination.Id, option => option.MapFrom(src => src.AttachmentID))
                 .ForMember(destination => destination.TaskID, option => option.MapFrom(src => src.TaskID))
                 .ForMember(destination => destination.ProjectID, option => option.MapFrom(src => src.ProjectID))
                 .ForMember(destination => destination.FileSizeKB, option => option.MapFrom(src => src.FileSize))
                 .ForMember(destination => destination.UserName, option => option.MapFrom(source => source.User != null ? source.User.Username : string.Empty));
            CreateMap<CreateAttachmentDto, Attachment>()
                .ForMember(dest => dest.FileSize, option => option.MapFrom(src => src.FileSizeKB));
            CreateMap<UpdateAttachmentDto, Attachment>();

            CreateMap<ProjectMember, ProjectMemberDto>()
                .ForMember(destination => destination.Id, option => option.MapFrom(src => src.ProjectMemberID))
                .ForMember(destination => destination.UserName, option => option.MapFrom(source => source.User != null ? source.User.Username : string.Empty))
                .ForMember(destination => destination.ProjectName, option => option.MapFrom(source => source.Project != null ? source.Project.ProjectName : string.Empty));
            CreateMap<CreateProjectMemberDto, ProjectMember>();
            CreateMap<UpdateProjectMemberRoleDto, ProjectMember>();
        }
    }
}
