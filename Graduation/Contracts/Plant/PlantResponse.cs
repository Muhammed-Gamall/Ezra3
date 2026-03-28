namespace Graduation.Contracts.Plant
{
    public record PlantResponse
    (
        int Id,
         string Name,
        string Describtion,
        PlantCategory Category,
        ClimateType Climate,
        PlantingLocationType SuitableLocation,
        string CareTips,
        double Price,
        double? PlantingServicePrice,

        IEnumerable<PhotoResponse> Images
     );
}
