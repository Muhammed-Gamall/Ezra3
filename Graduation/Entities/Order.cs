
namespace Graduation.Entities
{
    public class Order   :  AuditableEntity
    {
        public int Id { get; set; }

        public string? FarmerId { get; set; }
        public FarmerProfile Farmer { get; set; } = default!;  

        public double TotalAmount => Items.Count();
        public double PaidAmount => Items.Sum(i => (i.Plant.Price + i.Plant.PlantingServicePrice));
        public bool RequiresPlanting { get; set; } = false;
        public string HouseNum { get; set; } = string.Empty;
        public string LandMark { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double? PlantingLatitude { get; set; }
        public double? PlantingLongitude { get; set; }
        public DateOnly? ScheduledPlantingDate { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Notes { get; set; }


        // the order status will be used to track the progress of the order,
        // Admin & Farmer can update the status of the order, and the customer can only see the status of the order 
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> Items { get; set; } = [];
    }
}
