using MediatR;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Commands
{
    public class RegisterUserCommand(CreateUserDto userDto) : IRequest<int>
    {
        public CreateUserDto UserDto { get; set; } = userDto;
    }

    public class LoginUserCommand(LoginUserDto userDto) : IRequest<UserDto>
    {
        public LoginUserDto UserDto { get; set; } = userDto;
    }

    public class UpdateUserCommand(int id, UpdateUserDto userDto) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
        public UpdateUserDto UserDto { get; set; } = userDto;
    }

    public class DeleteUserCommand(int id) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
    }
}