using gamewiki.net.dto.request;
using gamewiki.net.interfaces;
using gamewiki.net.model;

namespace gamewiki.net.services;

public class GameService : IGameService {
    public Task<IReadOnlyList<Game>> GetAll() {
        return Task.FromResult<IReadOnlyList<Game>>(
            InMemoryData.Games
                .OrderBy(x => x.Name)
                .ToList()
        );
    }

    public Task<Game?> GetById(Guid id) {
        var game = InMemoryData.Games.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(game);
    }

    public Task<Game> Add(CreateGameRequest request) {
        ValidateProductFields(request.Name, request.ReleaseYear);

        var id = request.Id ?? Guid.NewGuid();
        
        if (InMemoryData.Games.Any(x => x.Id == id)) {
            throw new InvalidOperationException($"Игра с идентификатором {id} уже существует.");
        }
        
        var devExists = InMemoryData.Developers.Any(d => d.Id == request.DevId);
        if (!devExists) {
            throw new InvalidOperationException($"Разработчик с идентификатором {request.DevId} не найден.");
        }

        var entity = new Game {
            Id = id,
            Name = request.Name.Trim(),
            ReleaseYear = request.ReleaseYear,
            DevId = request.DevId
        };

        InMemoryData.Games.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Game?> Update(Guid id, UpdateGameRequest request) {
        ValidateProductFields(request.Name, request.ReleaseYear);

        var entity = InMemoryData.Games.FirstOrDefault(x => x.Id == id);
        if (entity is null) {
            return Task.FromResult<Game?>(null);
        }

        var devExists = InMemoryData.Developers.Any(d => d.Id == request.DevId);
        if (!devExists) {
            throw new InvalidOperationException($"Разработчик с идентификатором {request.DevId} не найден.");
        } 

        entity.Name = request.Name.Trim();
        entity.ReleaseYear = request.ReleaseYear;
        entity.DevId = request.DevId;
        
        return Task.FromResult<Game?>(entity);
    }

    public Task<bool> Delete(Guid id) {
        var entity = InMemoryData.Games.FirstOrDefault(x => x.Id == id);
        if (entity is null) {
            return Task.FromResult(false);
        }

        return Task.FromResult(InMemoryData.Games.Remove(entity));
    }

    private static void ValidateProductFields(string name, int releaseYear) {
        if (string.IsNullOrWhiteSpace(name)) {
            throw new ArgumentException("Название игры не должно быть пустым.");
        }

        if (releaseYear < 1958) {
            throw new ArgumentException("Первая игра вышла в 1958.");
        }
    }
}