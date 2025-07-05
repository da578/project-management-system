using AutoMapper;
using MediatR;
using ProjectManagement.Application.Commands;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Handlers
{
    public class RegisterUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUserByUsername = await _unitOfWork.Users.ReadByUsernameAsync(request.UserDto.Username);
            if (existingUserByUsername != null) throw new Exception($"Username '{request.UserDto.Username}' already exists.");

            var existingUserByEmail = await _unitOfWork.Users.ReadByEmailAsync(request.UserDto.Email);
            if (existingUserByEmail != null) throw new Exception($"Email '{request.UserDto.Email}' already registered.");

            var user = _mapper.Map<User>(request.UserDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.CompleteAsync();

            return user.UserID;
        }
    }

    public class LoginUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<LoginUserCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.ReadByUsernameAsync(request.UserDto.Username) ??
                throw new Exception($"Username '{request.UserDto.Username}' does not exist.");

            if (!BCrypt.Net.BCrypt.Verify(request.UserDto.Password, user.PasswordHash))
                throw new Exception("Invalid password.");

            return _mapper.Map<UserDto>(user);
        }
    }

    public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.ReadByIdAsync(request.Id) ??
            throw new Exception($"User with ID {request.Id} not found.");

            if (existingUser.Username != request.UserDto.Username)
            {
                var userWithNewUserName = await _unitOfWork.Users.ReadByUsernameAsync(request.UserDto.Username);
                if (userWithNewUserName != null && userWithNewUserName.UserID != request.Id)
                    throw new Exception($"Username '{request.UserDto.Username}' is already taken by another user.");
            }

            if (existingUser.Email != request.UserDto.Email)
            {
                var userWithNewEmail = await _unitOfWork.Users.ReadByEmailAsync(request.UserDto.Email);
                if (userWithNewEmail != null && userWithNewEmail.UserID != request.Id)
                    throw new Exception($"Email '{request.UserDto.Email}' is already registered by another user.");
            }

            _mapper.Map(request.UserDto, existingUser);
            existingUser.UpdatedAt = DateTime.Now;

            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _unitOfWork.Users.ReadByIdAsync(request.Id) ??
                throw new Exception($"User with ID {request.Id} not found.");

            _unitOfWork.Users.Delete(userToDelete);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }

    public class ReadUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadUserByIdQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(ReadUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.ReadByIdAsync(request.Id) ??
                throw new Exception($"User with ID {request.Id} not found.");
            return _mapper.Map<UserDto>(user);
        }
    }

    public class ReadAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserDto>> Handle(ReadAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.ReadAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }

    public class ReadUserByUsernameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadUserByUsernameQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(ReadUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.ReadByUsernameAsync(request.Username) ??
                throw new Exception($"User with username {request.Username} not found.");
            return _mapper.Map<UserDto>(user);
        }
    }

    public class ReadUserByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReadUserByEmailQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(ReadUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.ReadByUsernameAsync(request.Email) ??
                throw new Exception($"User with email {request.Email} not found.");
            return _mapper.Map<UserDto>(user);
        }
    }
}