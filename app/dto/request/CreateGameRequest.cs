namespace gamewiki.net.dto.request;

public record CreateGameRequest {
    public Guid? Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int ReleaseYear { get; init; }
    public Guid DevId { get; init; }
}