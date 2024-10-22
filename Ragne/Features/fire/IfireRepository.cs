namespace Ragne.Features.fire;

public interface IfireRepository
{
    // Create
    Task<Guid> CreateAsync(fireModel fireModel);

    // Read
    Task<fireModel> GetByIdAsync(Guid id);

    // Update
    Task UpdateAsync(Guid id, fireModel fireModel);

    // Delete
    Task DeleteAsync(Guid id);
}
