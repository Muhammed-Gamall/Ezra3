using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;


        [HttpGet("{farmerId}")]
        public async Task<IActionResult> AllOrders(int farmerId, CancellationToken cancellation)
        {
            var orders = await _orderService.GetAllOrdersForFarmer(farmerId, cancellation);
            return Ok(orders);
        }

        [HttpGet("AllForUser")]
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

        [HttpPost("{CustomerId}")]
        public async Task<IActionResult> CreateOrder(int CustomerId, OrderRequest request, CancellationToken cancellation)
        {
            var order = await _orderService.CreateOrderAsync(CustomerId, request, cancellation);
            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderRequest request, int orderId, CancellationToken cancellation)
        {
            var order = await _orderService.UpdateOrderAsync(request, orderId, cancellation);
            return Ok(order);
        }

        [HttpPut("{orderId}/ChangeStatus")]
        public async Task<IActionResult> ChangeOrderStatus(string status, int orderId, CancellationToken cancellation)
        {
            await _orderService.ChangeOrderStatus(status, orderId, cancellation);
            return NoContent();
        }
    }
}
