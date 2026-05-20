namespace Finanzas.Application.Dtos;

public record CreateCategoryRequest(string Name, string Color, string Icon);
public record UpdateCategoryRequest(string Color, string Icon);
public record CategoryResponse(Guid Id, string Name, string Color, string Icon);