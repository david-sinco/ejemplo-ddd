namespace Finanzas.Application.Dtos;


public record CreateAccountRequest(string Name, decimal InitialBalance, string Currency, string Icon);
public record UpdateAccountBalanceRequest(decimal NewAmount, string Reason);
public record AccountResponse(Guid Id, Guid UserId, string Name, decimal Balance, string Currency, string Icon, bool IsActive);