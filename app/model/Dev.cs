namespace gamewiki.net.model;

public class Dev {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid[]? GameIds { get; set; }
}