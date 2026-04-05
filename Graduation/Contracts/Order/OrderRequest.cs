namespace Graduation.Contracts.Order
{
    public record OrderRequest
     (

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
        List<OrderItemRequest> Items
        );
}
