namespace BI.Sales.Api.DTOs;

public record CartItemRequest(int ProductId, int Quantity);
public record CartItemUpdateRequest(int Quantity);
public record CartItemDto(int CartItemId, int ProductId, string ProductName, int Quantity, decimal UnitPriceSnapshot, decimal LineTotal);
public record CartDto(Guid CartId, IReadOnlyCollection<CartItemDto> Items, decimal SubTotal);
public record CheckoutRequest(string? Notes);
