namespace Graduation.Contracts.Farmer
{
    public record FarmerResponse
    (
          int Id,
         string Name,
        string? ProfessionalDescription,
        List<FarmerRatingResponse> Ratings
    );
}
