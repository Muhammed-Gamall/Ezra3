
namespace Graduation.Entities
{
    public class FarmerProfile  : AuditableEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; } = default!;

        public string Name { get; set; } = string.Empty;
        public string? ProfessionalDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<FarmerRating> Ratings { get; set; } = [];
        public ICollection <Order> Order { get; set; }=[];
        public ICollection <OrderItem> Items { get; set; }=[];

    }
}
