namespace Graduation.Contracts.Order
{
    public record OrderResponseForFarmer
    (
        double PaidAmount,
        double TotalAmount,
        string HouseNum,
        string LandMark,
        string Street,
        string District,
        string City,
         double? PlantingLatitude,
        double? PlantingLongitude,
        DateOnly? ScheduledPlantingDate,
        int Phone,
        string? Notes,
        bool RequiresPlanting,
        string? CustomerName,
        string Status,
        IEnumerable<OrderItemResponse> Items
        );
}
