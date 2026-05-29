namespace gamewiki.net.dto.request;

public record UpdateDevRequest {
    public Guid? Id { get; init; }
    public string Name { get; init; } = string.Empty;
}