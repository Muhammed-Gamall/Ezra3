namespace Graduation.Entities
{
    public class PlantPhoto 
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string? HashCode { get; set; }
        public bool IsActive { get; set; } = true;
        public int PlantId { get; set; } 
        public Plant Plant { get; set; } = default!;
    }
}
