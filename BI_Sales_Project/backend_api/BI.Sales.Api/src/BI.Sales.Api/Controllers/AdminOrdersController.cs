using BI.Sales.Api.DTOs;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/admin/orders")]
[Authorize(Roles = "Admin")]
public class AdminOrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public AdminOrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        return Ok(orders.Select(OrdersController.MapOrder));
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateOrderStatusRequest request)
    {
        var order = await _orderRepository.GetOrderAsync(id, string.Empty, true);
        if (order == null)
        {
            return NotFound();
        }

        order.Status = request.Status;
        await _orderRepository.UpdateAsync(order);
        return NoContent();
    }
}
