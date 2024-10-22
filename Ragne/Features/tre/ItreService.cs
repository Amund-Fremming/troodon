namespace Ragne.Features.tre;

public interface ItreService
{
    // Create
    Task<Guid> CreateAsync(treModel treModel);

    // Read
    Task<treModel> GetByIdAsync(Guid id);

    // Update
    Task UpdateAsync(Guid id, treModel treModel);

    // Delete
    Task DeleteAsync(Guid id);
}
