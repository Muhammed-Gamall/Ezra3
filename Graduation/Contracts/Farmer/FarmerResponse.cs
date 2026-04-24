namespace Graduation.Contracts.Farmer
{
    public record FarmerResponse
    (
          string UserId,
          string FName,
          string LName,
        string? ProfessionalDescription,
        IEnumerable<FarmerRatingResponse> Ratings
    );
}
