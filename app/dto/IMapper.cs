using gamewiki.net.dto.response;
using gamewiki.net.model;

namespace gamewiki.net.dto;

public interface IMapper {
    DevResponse Map(Dev dev);
    GameResponse Map(Game game);
}