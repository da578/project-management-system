
using MediatR;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Queries
{
    public class ReadAllUsersQuery : IRequest<IEnumerable<UserDto>> { }

    public class ReadUserByEmailQuery(string email) : IRequest<UserDto>
    {
        public string Email { get; set; } = email;
    }

    public class ReadUserByIdQuery(int id) : IRequest<UserDto>
    {
        public int Id { get; set; } = id;
    }

    public class ReadUserByUsernameQuery(string username) : IRequest<UserDto>
    {
        public string Username { get; set; } = username;
    }
}