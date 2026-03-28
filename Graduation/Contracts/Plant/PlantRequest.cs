namespace Graduation.Contracts.Plant
{
    public record PlantRequest
     (
        string Name,
        string Describtion,
        PlantCategory Category,
        ClimateType Climate,
        PlantingLocationType SuitableLocation,
        string CareTips,
        double Price,
        double PlantingServicePrice,

        List<IFormFile> Image

     );
}
