using System.Security.Claims;
using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.DTOs;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/cart")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly OltpEcommerceDbContext _context;

    public CartController(ICartRepository cartRepository, IOrderRepository orderRepository, OltpEcommerceDbContext context)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CartDto>> GetCart()
    {
        var userId = GetUserId();
        var cart = await _cartRepository.GetOrCreateCartAsync(userId);
        return Ok(MapCart(cart));
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem(CartItemRequest request)
    {
        var userId = GetUserId();
        var cart = await _cartRepository.GetOrCreateCartAsync(userId);
        var product = await _context.Products.FindAsync(request.ProductId);
        if (product == null)
        {
            return NotFound();
        }

        if (request.Quantity <= 0)
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        var existing = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
        if (existing != null)
        {
            existing.Quantity += request.Quantity;
            await _cartRepository.UpdateItemAsync(existing);
            return Ok();
        }

        var item = new CartItem
        {
            CartId = cart.CartId,
            ProductId = product.ProductId,
            Quantity = request.Quantity,
            UnitPriceSnapshot = product.UnitPrice
        };

        await _cartRepository.AddItemAsync(cart, item);
        return Ok();
    }

    [HttpPut("items/{itemId:int}")]
    public async Task<IActionResult> UpdateItem(int itemId, CartItemUpdateRequest request)
    {
        var userId = GetUserId();
        var item = await _cartRepository.GetCartItemAsync(itemId, userId);
        if (item == null)
        {
            return NotFound();
        }

        if (request.Quantity <= 0)
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        item.Quantity = request.Quantity;
        await _cartRepository.UpdateItemAsync(item);
        return Ok();
    }

    [HttpDelete("items/{itemId:int}")]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        var userId = GetUserId();
        var item = await _cartRepository.GetCartItemAsync(itemId, userId);
        if (item == null)
        {
            return NotFound();
        }

        await _cartRepository.RemoveItemAsync(item);
        return NoContent();
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<OrderDto>> Checkout(CheckoutRequest request)
    {
        var userId = GetUserId();
        var cart = await _cartRepository.GetOrCreateCartAsync(userId);
        if (!cart.Items.Any())
        {
            return BadRequest("Cart is empty.");
        }

        foreach (var item in cart.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null || product.StockQty < item.Quantity)
            {
                return BadRequest("Insufficient stock for one or more products.");
            }
        }

        foreach (var item in cart.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product != null)
            {
                product.StockQty -= item.Quantity;
            }
        }

        var order = new Order
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}",
            Status = "Pending",
            SubTotal = cart.Items.Sum(i => i.UnitPriceSnapshot * i.Quantity),
            ShippingFee = 10,
            Notes = request.Notes
        };
        order.Total = order.SubTotal + order.ShippingFee;

        foreach (var item in cart.Items)
        {
            order.Items.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPriceSnapshot = item.UnitPriceSnapshot,
                LineTotal = item.UnitPriceSnapshot * item.Quantity
            });
        }

        await _orderRepository.AddOrderAsync(order);

        _context.CartItems.RemoveRange(cart.Items);
        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new OrderDto(order.OrderId, order.OrderNumber, order.Status, order.CreatedAt, order.SubTotal, order.ShippingFee, order.Total, order.Notes,
            order.Items.Select(i => new OrderItemDto(i.OrderItemId, i.ProductId, i.Product?.Name ?? string.Empty, i.Quantity, i.UnitPriceSnapshot, i.LineTotal)).ToList()));
    }

    private CartDto MapCart(ShoppingCart cart)
    {
        var items = cart.Items.Select(i => new CartItemDto(i.CartItemId, i.ProductId, i.Product?.Name ?? string.Empty, i.Quantity, i.UnitPriceSnapshot, i.UnitPriceSnapshot * i.Quantity)).ToList();
        var subtotal = items.Sum(i => i.LineTotal);
        return new CartDto(cart.CartId, items, subtotal);
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
