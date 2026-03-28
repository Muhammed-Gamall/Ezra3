namespace Graduation.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<Order> Orders { get; set; } = [];

    }
}
