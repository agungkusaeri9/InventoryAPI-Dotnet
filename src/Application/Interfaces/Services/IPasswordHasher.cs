namespace InventoryApi_Dotnet.src.Application.Interfaces.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providePassword);
    }
}
