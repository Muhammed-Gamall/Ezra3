namespace Graduation.Contracts.Order
{
    public record OrderResponse 
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
        string? FarmerName,
        string Status,
        IEnumerable<OrderItemResponse> Items

        );
}
