namespace Graduation.Entities
{
    public class BundleItem
    {
      public int Id { get; set; }

        public int BundleId { get; set; }
        public Bundle Bundle { get; set; } = default!;
        public int Quantity { get; set; }

        public int PlantId { get; set; }
        public Plant Plant { get; set; } = default!;
    }
}
