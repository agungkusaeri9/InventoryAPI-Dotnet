using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.DTOs.User;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Application.Interfaces.Services;
using InventoryApi_Dotnet.src.Domain.Entities;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<PaginatedList<UserDTO>> GetAllAsync(int pageIndex, int pageSize, string? keyword = null)
        {
            var result = await _userRepository.GetAllAsync(pageIndex, pageSize, keyword);

            var items = result.Items.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            }).ToList();

            return new PaginatedList<UserDTO>(items, result.PageIndex, result.TotalPages, result.Total);
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }

        public async Task<UserDTO?> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username {username} not found.");
            }
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }

        public async Task<UserDTO?> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with email {email} not found.");
            }
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }

        public async Task<UserDTO> CreateAsync(CreateUserDTO createUserDto)
        {
            var existingUserByUsername = await _userRepository.GetByUsernameAsync(createUserDto.Username);
            if (existingUserByUsername != null)
            {
                throw new InvalidOperationException($"Username {createUserDto.Username} is already taken.");
            }
            var existingUserByEmail = await _userRepository.GetByEmailAsync(createUserDto.Email);
            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException($"Email {createUserDto.Email} is already registered.");
            }
            var user = new User
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Username = createUserDto.Username,
                Role = createUserDto.Role
            };

            user.Password = _passwordHasher.HashPassword(createUserDto.Password);

            var createdUser = await _userRepository.CreateAsync(user);
            return new UserDTO
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
                Username = createdUser.Username,
                Role = createdUser.Role
            };
        }

        public async Task<UserDTO> UpdateAsync(int id, UpdateUserDTO updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.Name = updateUserDto.Name ?? user.Name;
            user.Email = updateUserDto.Email ?? user.Email;
            user.Username = updateUserDto.Username ?? user.Username;
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                user.Password = _passwordHasher.HashPassword(updateUserDto.Password);
            }
            user.Role = updateUserDto.Role ?? user.Role;

            var updatedUser = await _userRepository.UpdateAsync(user);
            return new UserDTO
            {
                Id = updatedUser.Id,
                Name = updatedUser.Name,
                Email = updatedUser.Email,
                Username = updatedUser.Username,
                Role = updatedUser.Role
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return await _userRepository.DeleteAsync(user);
        }
    }
}