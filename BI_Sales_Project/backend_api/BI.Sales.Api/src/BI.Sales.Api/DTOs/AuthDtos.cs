namespace BI.Sales.Api.DTOs;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password);
public record AuthResponse(string Token, DateTime ExpiresAt);
