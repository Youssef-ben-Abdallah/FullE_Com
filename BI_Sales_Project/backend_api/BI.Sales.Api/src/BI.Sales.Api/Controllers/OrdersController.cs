using System.Security.Claims;
using BI.Sales.Api.DTOs;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetMyOrders()
    {
        var userId = GetUserId();
        var orders = await _orderRepository.GetOrdersForUserAsync(userId);
        return Ok(orders.Select(MapOrder));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var userId = GetUserId();
        var isAdmin = User.IsInRole("Admin");
        var order = await _orderRepository.GetOrderAsync(id, userId, isAdmin);
        if (order == null)
        {
            return NotFound();
        }

        return Ok(MapOrder(order));
    }

    public static OrderDto MapOrder(Entities.OltpEcommerce.Order order)
    {
        var items = order.Items.Select(i => new OrderItemDto(i.OrderItemId, i.ProductId, i.Product?.Name ?? string.Empty, i.Quantity, i.UnitPriceSnapshot, i.LineTotal)).ToList();
        return new OrderDto(order.OrderId, order.OrderNumber, order.Status, order.CreatedAt, order.SubTotal, order.ShippingFee, order.Total, order.Notes, items);
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
