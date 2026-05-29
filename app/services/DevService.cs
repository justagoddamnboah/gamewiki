using gamewiki.net.dto.request;
using gamewiki.net.interfaces;
using gamewiki.net.model;

namespace gamewiki.net.services;

public class DevService : IDevService {
    public Task<IReadOnlyList<Dev>> GetAll() {
        return Task.FromResult<IReadOnlyList<Dev>>(
            InMemoryData.Developers
                .OrderBy(x => x.Name)
                .ToList()
        );
    }

    public Task<Dev?> GetById(Guid id) {
        var dev = InMemoryData.Developers.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(dev);
    }

    public Task<Dev> Add(CreateDevRequest request) {
        ValidateUserFields(request.Name);

        var id = request.Id ?? Guid.NewGuid();
        
        if (InMemoryData.Developers.Any(x => x.Id == id)) {
            throw new InvalidOperationException($"Разработчик с идентификатором {id} уже существует.");
        }

        var entity = new Dev {
            Id = id,
            Name = request.Name.Trim()
        };

        InMemoryData.Developers.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Dev?> Update(Guid id, UpdateDevRequest request) {
        ValidateUserFields(request.Name);

        var entity = InMemoryData.Developers.FirstOrDefault(x => x.Id == id);
        if (entity is null) {
            return Task.FromResult<Dev?>(null);
        }

        entity.Name = request.Name.Trim();
        return Task.FromResult<Dev?>(entity);
    }

    public Task<bool> Delete(Guid id) {
        var entity = InMemoryData.Developers.FirstOrDefault(x => x.Id == id);
        if (entity is null) {
            return Task.FromResult(false);
        }

        var hasGames = InMemoryData.Games.Any(g => g.DevId == id);
        if (hasGames) {
            throw new InvalidOperationException("Нельзя удалить разработчика: есть связанные игры.");
        }

        return Task.FromResult(InMemoryData.Developers.Remove(entity));
    }

    private static void ValidateUserFields(string name) {
        if (string.IsNullOrWhiteSpace(name)) {
            throw new ArgumentException("Имя разработчика не должно быть пустым.");
        }
    }
}