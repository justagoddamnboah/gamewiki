using gamewiki.net.model;

namespace gamewiki.net;

public static class InMemoryData {
    public static List<Dev> Developers { get; } = new();
    public static List<Game> Games { get; } = new();
    
    static InMemoryData() {
        var rockstarId = Guid.NewGuid();
        var cdprId = Guid.NewGuid();
        
        Developers.Add(new Dev { Id = rockstarId, Name = "Rockstar North" });
        Developers.Add(new Dev { Id = cdprId, Name = "CD Projekt Red" });
        
        Games.Add(new Game { Id = Guid.NewGuid(), Name = "Red Dead Redemption", ReleaseYear = 2010, DevId = rockstarId });
        Games.Add(new Game { Id = Guid.NewGuid(), Name = "The Witcher 3: Wild Hunt", ReleaseYear = 2015, DevId = cdprId });
        Games.Add(new Game { Id = Guid.NewGuid(), Name = "Grand Theft Auto IV", ReleaseYear = 2008, DevId = rockstarId });
    }
}