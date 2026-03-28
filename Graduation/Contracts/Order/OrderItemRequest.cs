namespace Graduation.Contracts.Order
{
    public record OrderItemRequest
    (
        int PlantId,
        double UnitPrice,
        double PlantingServicePrice
    );
}
