namespace Ragne.Features.seks;

public interface IseksRepository
{
    // Create
    Task<Guid> CreateAsync(seksModel seksModel);

    // Read
    Task<seksModel> GetByIdAsync(Guid id);

    // Update
    Task UpdateAsync(Guid id, seksModel seksModel);

    // Delete
    Task DeleteAsync(Guid id);
}
