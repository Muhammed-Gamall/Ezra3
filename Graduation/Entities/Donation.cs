namespace Graduation.Entities
{
    public class Donation : AuditableEntity
    {
        public int Id { get; set; }
        public DonationCategory TreeType { get; set; }
        public double Amount  { get; set; }
        public string Street  { get; set; } = string.Empty;
        public string District{ get; set; } = string.Empty;
        public string City     { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;

    }
}
