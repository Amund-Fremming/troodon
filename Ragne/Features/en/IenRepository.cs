namespace Ragne.Features.en;

public interface IenRepository
{
    // Create
    Task<Guid> CreateAsync(enModel enModel);

    // Read
    Task<enModel> GetByIdAsync(Guid id);

    // Update
    Task UpdateAsync(Guid id, enModel enModel);

    // Delete
    Task DeleteAsync(Guid id);
}
