namespace gamewiki.net.dto.response;

public record DevGamesResponse {
    public Guid DevId { get; init; }
    public int GamesCount { get; init; }
    public Guid[]? GameIds { get; init; }
}