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
        string? FarmerFName,
        string? FarmerLName,
        string Status,
        IEnumerable<OrderItemResponse> Items

        );
}
