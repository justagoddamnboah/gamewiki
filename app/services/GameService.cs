using gamewiki.net.database;
using gamewiki.net.dto.request;
using gamewiki.net.interfaces;
using gamewiki.net.model;
using Microsoft.EntityFrameworkCore;

namespace gamewiki.net.services;

public class GameService(WikiDbContext db) : IGameService {
    public async Task<IReadOnlyList<Game>> GetAllAsync()
        => await db.Games
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();

    public async Task<Game?> GetByIdAsync(Guid id)
        => await db.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Game> AddAsync(CreateGameRequest request) {
        ValidateProductFields(request.Name, request.ReleaseYear);

        var id = request.Id ?? Guid.NewGuid();
        if (await db.Games.AnyAsync(x => x.Id == id)) {
            throw new InvalidOperationException($"Игра с идентификатором {id} уже существует.");
        }

        var entity = new Game {
            Id = id,
            Name = request.Name.Trim(),
            ReleaseYear = request.ReleaseYear,
            DevId = request.DevId
        };

        db.Games.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Game?> UpdateAsync(Guid id, UpdateGameRequest request) {
        ValidateProductFields(request.Name, request.ReleaseYear);

        var entity = await db.Games.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) {
            return null;
        }

        entity.Name = request.Name.Trim();
        entity.ReleaseYear = request.ReleaseYear;
        entity.DevId = request.DevId;
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id) {
        var entity = await db.Games.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) {
            return false;
        }

        db.Games.Remove(entity);
        await db.SaveChangesAsync();
        return true;
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