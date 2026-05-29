namespace gamewiki.net.dto.response;

public record DevResponse {
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}