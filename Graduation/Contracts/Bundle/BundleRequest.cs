namespace Graduation.Contracts.Bundle
{
    public record BundleRequest
    (
     string Name ,
     string? Description,
     IFormFile? Photo ,
     string Tag,
     double Price ,
     bool PlantIncluded ,
     List<BundleItemRequest> Items
        );
}
