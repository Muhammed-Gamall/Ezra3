namespace Graduation.Contracts.Order
{
    public record OrderResponse 
    (
        int Id,
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
        string Phone,
        string? Notes,
        bool RequiresPlanting,
        string? FarmerName,
        string Status,
        IEnumerable<OrderItemResponse> Items

        );
}
