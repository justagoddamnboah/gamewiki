using gamewiki.net.dto.request;
using gamewiki.net.model;

namespace gamewiki.net.interfaces;

public interface IDevService {
    Task<IReadOnlyList<Dev>> GetAllAsync();
    Task<Dev?> GetByIdAsync(Guid id);
    Task<Dev> AddAsync(CreateDevRequest request);
    Task<Dev?> UpdateAsync(Guid id, UpdateDevRequest request);
    Task<bool> DeleteAsync(Guid id);
}