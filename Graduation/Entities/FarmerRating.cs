namespace Graduation.Entities
{
    public class FarmerRating : AuditableEntity
    {
        public int Id { get; set; } 
        public double Rating { get; set; } = 0;
        public string? Review { get; set; }

        //don't need to store the customer id because we can get it from the audit logging
        public string FarmerId { get; set; } = string.Empty;
        public FarmerProfile Farmer { get; set; } = default!;
    }
}
