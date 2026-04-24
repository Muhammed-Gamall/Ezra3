using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;


        [HttpGet("AllForFarmer")]
        [Authorize]
        public async Task<IActionResult> AllOrders( CancellationToken cancellation)
        {
            var orders = await _orderService.GetAllOrdersForFarmer(cancellation);
            return Ok(orders);
        }

        [HttpGet("AllForUser")]
        [Authorize]
        public async Task<IActionResult> AllOrdersForUser(CancellationToken cancellation)
        {
            var orders = await _orderService.GetAllOrdersAsync(cancellation);
            return Ok(orders);
        }

        [HttpGet("{id}/OrderForUser")]
        public async Task<IActionResult> Orders(int orderId, CancellationToken cancellation)
        {
            var orders = await _orderService.GetOrderAsync(orderId, cancellation);
            return Ok(orders);
        }

        [HttpGet("{orderId}/OrderForFarmer")]
        public async Task<IActionResult> GetAllOrders(int orderId, CancellationToken cancellation)
        {
            var orders = await _orderService.GetOrderForFarmer(orderId, cancellation);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder( OrderRequest request, CancellationToken cancellation)
        {
            var order = await _orderService.CreateOrderAsync( request, cancellation);
            return Ok(order);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(OrderRequest request, int orderId, CancellationToken cancellation)
        {
            var result = await _orderService.UpdateOrderAsync(request, orderId, cancellation);
            return result ? NoContent() : NotFound("order not found");
        }

        [HttpPut("{orderId}/ChangeStatus")]
        [Authorize]
        public async Task<IActionResult> ChangeOrderStatus(string status, int orderId, CancellationToken cancellation)
        {
           var result = await _orderService.ChangeOrderStatus(status, orderId, cancellation);

            return result ? NoContent() : NotFound("order not found");
        }

        [HttpPut("{orderId}/AddFarmer")]
        [Authorize]
        public async Task<IActionResult> AddFarmer(string farmerId, int orderId, CancellationToken cancellation)
        {
            var result = await _orderService.AddFarmer(farmerId, orderId, cancellation);
            return result ? NoContent() : NotFound("order not found");

        }
    }
}
