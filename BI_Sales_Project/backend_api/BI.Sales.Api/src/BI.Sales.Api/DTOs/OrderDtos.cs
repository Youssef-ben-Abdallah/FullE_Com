namespace BI.Sales.Api.DTOs;

public record OrderItemDto(int OrderItemId, int ProductId, string ProductName, int Quantity, decimal UnitPriceSnapshot, decimal LineTotal);
public record OrderDto(int OrderId, string OrderNumber, string Status, DateTime CreatedAt, decimal SubTotal, decimal ShippingFee, decimal Total, string? Notes, IReadOnlyCollection<OrderItemDto> Items);
public record UpdateOrderStatusRequest(string Status);
