namespace Ragne.Features.fem;

public interface IfemService
{
    // Create
    Task<Guid> CreateAsync(femModel femModel);

    // Read
    Task<femModel> GetByIdAsync(Guid id);

    // Update
    Task UpdateAsync(Guid id, femModel femModel);

    // Delete
    Task DeleteAsync(Guid id);
}
