using gamewiki.net.dto.request;
using gamewiki.net.model;

namespace gamewiki.net.interfaces;

public interface IGameService {
    Task<IReadOnlyList<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(Guid id);
    Task<Game> AddAsync(CreateGameRequest request);
    Task<Game?> UpdateAsync(Guid id, UpdateGameRequest request);
    Task<bool> DeleteAsync(Guid id);
}