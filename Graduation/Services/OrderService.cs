using CloudinaryDotNet.Core;

namespace Graduation.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderResponseForFarmer>> GetAllOrdersForFarmer(int farmerId, CancellationToken cancellation);
        public Task<IEnumerable<OrderResponse>> GetAllOrdersAsync( CancellationToken cancellation);
        public Task<OrderResponseForFarmer?> GetOrderForFarmer(int orderId, CancellationToken cancellation);
        public Task<OrderResponse?> GetOrderAsync(int orderId, CancellationToken cancellation);
        public Task<OrderResponse> CreateOrderAsync(int CustomerId,OrderRequest request, CancellationToken cancellation);
        public Task<OrderResponse> UpdateOrderAsync(OrderRequest request, int orderId, CancellationToken cancellation);
        public Task ChangeOrderStatus(string status , int orderId, CancellationToken cancellation);
    }
    public class OrderService(ApplicationDbContext context , IHttpContextAccessor accessor) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _accessor = accessor;


        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellation)
        {
            var userId = GetUserId();

            var orders = await _context.Orders.Where(o => o.UserId == userId && o.Status != OrderStatus.Cancelled)
                .Include(o => o.Items).AsNoTracking()
                .Select(o => new OrderResponse
                (
                    o.PaidAmount,
                    o.TotalAmount,
                     o.HouseNum,
                     o.LandMark,
                     o.Street,
                     o.District,
                     o.City,
                     o.PlantingLatitude,
                     o.PlantingLongitude,
                     o.ScheduledPlantingDate,
                     o.Phone,
                     o.Notes,
                     o.RequiresPlanting,
                     o.Farmer.Name,
                     o.Status.ToString(),
                     o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name, i.Plant.Price, i.Plant.PlantingServicePrice))
      
                )).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<OrderResponseForFarmer>> GetAllOrdersForFarmer(int farmerId, CancellationToken cancellation)
        {
            var orders = await _context.Orders.Where(o => o.FarmerId == farmerId && o.Status != OrderStatus.Cancelled)
            .Include(o => o.Items).AsNoTracking()
            .Select(o => new OrderResponseForFarmer
            (
                o.PaidAmount,
                o.TotalAmount,
                 o.HouseNum,
                 o.LandMark,
                 o.Street,
                 o.District,
                 o.City,
                 o.PlantingLatitude,
                 o.PlantingLongitude,
                 o.ScheduledPlantingDate,
                 o.Phone,
                 o.Notes,
                 o.RequiresPlanting,
                 o.Status.ToString(),
                 o.Customer.Name,
                 o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name, i.Plant.Price, i.Plant.PlantingServicePrice))

            )).ToListAsync();

            return orders;
        }

        public async Task<OrderResponse?> GetOrderAsync(int orderId, CancellationToken cancellation)
        {

            var orders = await _context.Orders.Where(o => o.Id == orderId)
                .Include(o => o.Items).AsNoTracking()
                .Select(o => new OrderResponse
                (
                    o.PaidAmount,
                    o.TotalAmount,
                     o.HouseNum,
                     o.LandMark,
                     o.Street,
                     o.District,
                     o.City,
                     o.PlantingLatitude,
                     o.PlantingLongitude,
                     o.ScheduledPlantingDate,
                     o.Phone,
                     o.Notes,
                     o.RequiresPlanting,
                     o.Farmer.Name,
                     o.Status.ToString(),
                     o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name, i.Plant.Price, i.Plant.PlantingServicePrice))

                )).SingleOrDefaultAsync(cancellation);
            return orders;
        }

        public async Task<OrderResponseForFarmer?> GetOrderForFarmer(int orderId, CancellationToken cancellation)
        {
            var orders = await _context.Orders.Where(o => o.Id == orderId)
          .Include(o => o.Items).AsNoTracking()
          .Select(o => new OrderResponseForFarmer
          (
              o.PaidAmount,
              o.TotalAmount,
               o.HouseNum,
               o.LandMark,
               o.Street,
               o.District,
               o.City,
               o.PlantingLatitude,
               o.PlantingLongitude,
               o.ScheduledPlantingDate,
               o.Phone,
               o.Notes,
               o.RequiresPlanting,
               o.Status.ToString(),
               o.Customer.Name,
               o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name, i.Plant.Price, i.Plant.PlantingServicePrice))

          )).SingleOrDefaultAsync(cancellation);

            return orders;
        }
        public async Task<OrderResponse> CreateOrderAsync(int CustomerId,  OrderRequest request, CancellationToken cancellation)
        {
            var userId= GetUserId();

            var order = request.Adapt<Order>();
            order.UserId = userId;
            order.CustomerId = CustomerId;
           

            await _context.Orders.AddAsync(order, cancellation);
            await _context.SaveChangesAsync(cancellation);

            return order.Adapt<OrderResponse>();
        }

        public async Task<OrderResponse> UpdateOrderAsync(OrderRequest request, int orderId, CancellationToken cancellation)
        {
            var order =await _context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);

            var updatedOrder = request.Adapt(order);

            _context.Orders.Update(updatedOrder!);
            await _context.SaveChangesAsync(cancellation);

            return updatedOrder!.Adapt<OrderResponse>();
        }

        public async Task ChangeOrderStatus(string status,  int orderId, CancellationToken cancellation)
        {
            var order =await _context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);

            order!.Status = Enum.Parse<OrderStatus>(status);
            //order!.Status = status switch
            //{
            //    "Pending" => OrderStatus.Pending,
            //    "Confirmed" => OrderStatus.Confirmed,
            //    "InProgress" => OrderStatus.InProgress,
            //    "Completed" => OrderStatus.Completed,
            //    "Cancelled" => OrderStatus.Cancelled,
            //    _ => order.Status
            //};

            return;
        }

        public string GetUserId()
        {
            return _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

    }
}
