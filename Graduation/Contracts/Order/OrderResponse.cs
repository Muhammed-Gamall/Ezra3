namespace Graduation.Contracts.Order
{
    public record OrderResponse 
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
        string? FarmerName,
        string? FarmerRating,
        string Status,
        List<OrderItemResponse> Items

        );
}
