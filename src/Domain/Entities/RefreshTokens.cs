using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi_Dotnet.src.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public string? ReplacedByToken { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }

}
