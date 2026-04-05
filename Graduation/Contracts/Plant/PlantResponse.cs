namespace Graduation.Contracts.Plant
{
    public record PlantResponse
    (
        int Id,
         string Name,
        string Describtion,
        string Category,
        string Climate,
        string SuitableLocation,
        string CareTips,
        double Price,
        double? PlantingServicePrice,

        IEnumerable<PhotoResponse> Images
     );
}
