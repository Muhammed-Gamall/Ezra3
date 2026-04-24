using CloudinaryDotNet.Core;

namespace Graduation.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderResponseForFarmer>> GetAllOrdersForFarmer( CancellationToken cancellation);
        public Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellation);
        public Task<OrderResponseForFarmer?> GetOrderForFarmer(int orderId, CancellationToken cancellation);
        public Task<OrderResponse?> GetOrderAsync(int orderId, CancellationToken cancellation);
        public Task<OrderResponse> CreateOrderAsync( OrderRequest request, CancellationToken cancellation);
        public Task<bool> UpdateOrderAsync(OrderRequest request, int orderId, CancellationToken cancellation);
        public Task<bool> ChangeOrderStatus(string status, int orderId, CancellationToken cancellation);
        public Task<bool> AddFarmer(string FarmerId, int orderId, CancellationToken cancellation);
    }
    public class OrderService(ApplicationDbContext context, IHttpContextAccessor accessor) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _accessor = accessor;


        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellation)
        {
            var userId = GetUserId();

            var orders = await _context.Orders.Where(o => o.CreatedById == userId && o.Status != OrderStatus.Cancelled)
                .Include(o => o.Items)
                .ThenInclude(i => i.Plant).AsNoTracking()
                .Select(o => new OrderResponse
                (
                    o.Id,
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
                     o.Farmer.User.FName,
                     o.Farmer.User.LName,
                     o.Status.ToString(),
                     o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name))

                )).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<OrderResponseForFarmer>> GetAllOrdersForFarmer( CancellationToken cancellation)
        {
            var userId = GetUserId();

            var orders = await _context.Orders.Where(o => o.FarmerId == userId && o.Status != OrderStatus.Cancelled)
            .Include(o => o.Items)
            .ThenInclude(i => i.Plant).AsNoTracking()
            .Select(o => new OrderResponseForFarmer
            (
                o.Id,
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
                 o.CreatedBy.FName,
                 o.CreatedBy.LName,
                 o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name))

            )).ToListAsync();

            return orders;
        }

        public async Task<OrderResponse?> GetOrderAsync(int orderId, CancellationToken cancellation)
        {

            var orders = await _context.Orders.Where(o => o.Id == orderId)
                .Include(o => o.Items).
                ThenInclude(i => i.Plant).AsNoTracking()
                .Select(o => new OrderResponse
                (
                    o.Id,
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
                     o.Farmer.User.FName,
                     o.Farmer.User.LName,
                     o.Status.ToString(),
                     o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name))

                )).SingleOrDefaultAsync(cancellation);
            return orders;
        }

        public async Task<OrderResponseForFarmer?> GetOrderForFarmer(int orderId, CancellationToken cancellation)
        {
            var orders = await _context.Orders.Where(o => o.Id == orderId)
          .Include(o => o.Items)
          .ThenInclude(i => i.Plant).AsNoTracking()
          .Select(o => new OrderResponseForFarmer
          (
                    o.Id,
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
               o.CreatedBy.FName,
               o.CreatedBy.LName,
               o.Items.Select(i => new OrderItemResponse(i.Id, i.Plant.Name))

          )).SingleOrDefaultAsync(cancellation);

            return orders;
        }
        public async Task<OrderResponse> CreateOrderAsync( OrderRequest request, CancellationToken cancellation)
        {
            var order = request.Adapt<Order>();

            await _context.Orders.AddAsync(order, cancellation);
            await _context.SaveChangesAsync(cancellation);

            var orderwithPlants = await _context.Orders.Where(o => o.Id == order.Id)
                .Include(o => o.Items)
                .ThenInclude(i => i.Plant).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == order.Id, cancellation);


            return orderwithPlants!.Adapt<OrderResponse>();
        }

        public async Task<bool> UpdateOrderAsync(OrderRequest request, int orderId, CancellationToken cancellation)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            return false;

            var updatedOrder = request.Adapt(order);

            _context.Orders.Update(updatedOrder!);
            await _context.SaveChangesAsync(cancellation);

            return true;
        }

        public async Task<bool> ChangeOrderStatus(string status, int orderId, CancellationToken cancellation)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
                return false;

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

            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellation);

            return true;
        }
        public async Task<bool> AddFarmer(string FarmerId, int orderId, CancellationToken cancellation)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
                return false;

            var farmer = await _context.FarmerProfiles.AnyAsync(o => o.UserId == FarmerId);
            if (!farmer)
                return false;

            order!.FarmerId = FarmerId;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellation);

            return true;
        }

        public string GetUserId()
        {
            return _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

    }
}
