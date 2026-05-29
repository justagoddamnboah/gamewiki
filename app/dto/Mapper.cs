using gamewiki.net.dto.response;
using gamewiki.net.model;

namespace gamewiki.net.dto;

public class Mapper : IMapper {
    public DevResponse Map(Dev dev) => new() {
        Id = dev.Id,
        Name = dev.Name
    };
    
    public GameResponse Map(Game game) => new() {
        Id = game.Id,
        Name = game.Name,
        ReleaseYear = game.ReleaseYear,
        DevId = game.DevId
    };
}