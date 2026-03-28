
namespace Graduation.Entities
{
    public class Plant : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Describtion { get; set; } = string.Empty;
        public PlantCategory Category { get; set; }
        public ClimateType Climate { get; set; }
        public PlantingLocationType SuitableLocation { get; set; }
        public string? CareTips { get; set; } = string.Empty;
        public double Price { get; set; }
        public double? PlantingServicePrice { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PlantPhoto> Images { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];

    }
}
