namespace Graduation.Contracts.Order
{
    public record OrderResponseForFarmer
    (
        string? HouseNum,
        string? LandMark,
        string? Street,
        string? District,
        string? City,
         double? PlantingLatitude,
        double? PlantingLongitude,
        DateOnly? ScheduledPlantingDate,
        int Phone,
        string? Notes,
        bool RequiresPlanting,
        string? CustomerName,
        string Status,
        List<OrderItemResponse> Items
        );
}
