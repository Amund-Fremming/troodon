namespace Ragne.Features.to;

public interface ItoRepository
{
    // Create
    Task<Guid> CreateAsync(toModel toModel);

    // Read
    Task<toModel> GetByIdAsync(Guid id);

    // Update
    Task UpdateAsync(Guid id, toModel toModel);

    // Delete
    Task DeleteAsync(Guid id);
}
