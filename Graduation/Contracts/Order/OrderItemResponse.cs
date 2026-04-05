namespace Graduation.Contracts.Order
{
    public record OrderItemResponse
    (
        int Id,
        string PlantName,
        double UnitPrice,
        double PlantingServicePrice
    );
}
