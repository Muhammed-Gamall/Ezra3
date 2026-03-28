namespace Graduation.Contracts.Farmer
{
    public record FarmerRatingResponse
    (
       int Id,
       double Rating,
       string? Review
     );
}
