namespace Graduation.Contracts.Order
{
    public record OrderItemResponse
    (
        int Id,
        int PlantId,
        double UnitPrice,
        double PlantingServicePrice
    );
}
