using gamewiki.net.dto.request;
using gamewiki.net.model;

namespace gamewiki.net.interfaces;

public interface IGameService {
    Task<IReadOnlyList<Game>> GetAll();
    Task<Game?> GetById(Guid id);
    Task<Game> Add(CreateGameRequest request);
    Task<Game?> Update(Guid id, UpdateGameRequest request);
    Task<bool> Delete(Guid id);
}