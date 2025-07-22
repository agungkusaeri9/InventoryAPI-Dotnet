using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi_Dotnet.src.Application.Common.Pagination;
using InventoryApi_Dotnet.src.Application.Interfaces.Repositories;
using InventoryApi_Dotnet.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Dotnet.src.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<User>> GetAllAsync(int pageIndex, int pageSize, string? keyword = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(u => u.Name.Contains(keyword) || u.Username.Contains(keyword) || u.Email.Contains(keyword) || u.Role.ToString().Contains(keyword));
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var items = await query
                .OrderBy(u => u.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new User
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Username = u.Username,
                    Role = u.Role
                })
                .ToListAsync();

            return new PaginatedList<User>(items, pageIndex, totalPages, count);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}