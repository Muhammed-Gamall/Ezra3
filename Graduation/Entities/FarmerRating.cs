namespace Graduation.Entities
{
    public class FarmerRating : AuditableEntity
    {
        public int Id { get; set; } 
        public double Rating { get; set; } = 0;
        public string? Review { get; set; }

        public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; } = default!;

        public int FarmerId { get; set; } 
        public FarmerProfile Farmer { get; set; } = default!;
    }
}
