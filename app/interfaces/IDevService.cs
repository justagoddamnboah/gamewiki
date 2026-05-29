using gamewiki.net.dto.request;
using gamewiki.net.model;

namespace gamewiki.net.interfaces;

public interface IDevService {
    Task<IReadOnlyList<Dev>> GetAll();
    Task<Dev?> GetById(Guid id);
    Task<Dev> Add(CreateDevRequest request);
    Task<Dev?> Update(Guid id, UpdateDevRequest request);
    Task<bool> Delete(Guid id);
}