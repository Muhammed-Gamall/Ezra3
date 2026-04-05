namespace Graduation.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public int PlantId { get; set; }
        public Plant Plant { get; set; } = default!;

    }
}
