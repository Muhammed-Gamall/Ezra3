namespace Graduation.Entities
{
    public class Blog : AuditableEntity
    {
        public int Id { get; set; }
        public string Title    { get; set; } = string.Empty;
        public string Content   { get; set; } = string.Empty;
        public string? Image   { get; set; }
        public string? HashCode { get; set; }
        public string? Excerpt { get; set; }  
        public string? Category { get; set; }
        public string? Author   { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
