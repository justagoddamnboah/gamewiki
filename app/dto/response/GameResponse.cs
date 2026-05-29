namespace gamewiki.net.dto.response;

public record GameResponse {
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int ReleaseYear { get; init; }
    public Guid? DevId { get; init; }
}