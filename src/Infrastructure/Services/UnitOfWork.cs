using InventoryApi_Dotnet.src.Application.Interfaces;
using InventoryApi_Dotnet.src.Infrastructure.Persistence;

namespace InventoryApi_Dotnet.src.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
