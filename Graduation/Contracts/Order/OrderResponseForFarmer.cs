namespace Graduation.Contracts.Order
{
    public record OrderResponseForFarmer
    (
          int  Id,
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
        string CustomerFName,
        string CustomerLName,
        string Status,
        IEnumerable<OrderItemResponse> Items
        );
}
