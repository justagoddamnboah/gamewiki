namespace gamewiki.net.model;

public class Game {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public Guid DevId { get; set; }
}