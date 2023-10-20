namespace Terminal.Routing.Resolving;

public record RouteResolveResult(bool Success, object? Result, Exception? exception);
