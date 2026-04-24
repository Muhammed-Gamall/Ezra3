
namespace Graduation.Entities
{
    public class FarmerProfile 
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = default!;

        public string? ProfessionalDescription { get; set; } = string.Empty;

        public ICollection<FarmerRating> Ratings { get; set; } = [];
        public ICollection <Order> Orders { get; set; }=[];

    }
}
