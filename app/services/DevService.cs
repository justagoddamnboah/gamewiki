using gamewiki.net.dto.request;
using gamewiki.net.interfaces;
using gamewiki.net.database;
using gamewiki.net.model;
using Microsoft.EntityFrameworkCore;

namespace gamewiki.net.services;

public class DevService(WikiDbContext db) : IDevService {
    public async Task<IReadOnlyList<Dev>> GetAllAsync()
        => await db.Devs
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();

    public async Task<Dev?> GetByIdAsync(Guid id)
        => await db.Devs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Dev> AddAsync(CreateDevRequest request) {
        ValidateUserFields(request.Name);

        var id = request.Id ?? Guid.NewGuid();
        if (await db.Devs.AnyAsync(x => x.Id == id)) {
            throw new InvalidOperationException($"Разработчик с идентификатором {id} уже существует.");
        }

        var entity = new Dev {
            Id = id,
            Name = request.Name.Trim()
        };

        db.Devs.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Dev?> UpdateAsync(Guid id, UpdateDevRequest request) {
        ValidateUserFields(request.Name);

        var entity = await db.Devs.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) {
            return null;
        }

        entity.Name = request.Name.Trim();
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id) {
        var entity = await db.Devs.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) {
            return false;
        }

        var hasGames = await db.Games.AnyAsync(p => p.Id == id);
        if (hasGames) {
            throw new InvalidOperationException("Нельзя удалить разработчика: есть связанные игры.");
        }

        db.Devs.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static void ValidateUserFields(string Name) {
        if (string.IsNullOrWhiteSpace(Name)) {
            throw new ArgumentException("Имя пользователя не должно быть пустым.");
        }
    }
}