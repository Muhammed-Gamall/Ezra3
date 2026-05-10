namespace Graduation.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsCasualUser { get; set; } = false;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public FarmerProfile Farmer { get; set; } = default!;

    }
}
