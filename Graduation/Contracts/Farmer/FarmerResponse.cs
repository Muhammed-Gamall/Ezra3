namespace Graduation.Contracts.Farmer
{
    public record FarmerResponse
    (
          string UserId,
          string FullName,
        string? ProfessionalDescription,
        IEnumerable<FarmerRatingResponse> Ratings
    );
}
