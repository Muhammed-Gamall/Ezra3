namespace Graduation.Contracts.Bundle
{
    public record BundleResponse
    (
        int Id,
     string Name,
     string? Description,
     string? Image,
     string Tag,
     double OriginalPrice,
     double Price,
     bool? PlantIncluded,
     IEnumerable<BundleItemRequest> Items

        );
}
