namespace Graduation.Entities
{
    public class Bundle : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public string? Image { get; set; }
        public string? HashCode { get; set; }
        public string Tag { get; set; } = string.Empty;
        public double Price { get; set; } 
        public double OriginalPrice => Items.Sum(i => ((i.Plant.Price)
                          + (PlantIncluded ? i.Plant.PlantingServicePrice  : 0)) * i.Quantity);
        public bool PlantIncluded { get; set; } = false;

        public bool IsActive { get; set; } = true;
        public ICollection<BundleItem> Items { get; set; } = new List<BundleItem>();

    }
}
