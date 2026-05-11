namespace Graduation.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsDeleted { get; set; } = false;
        public bool IsDefault { get; set; } = false;
    }
}
