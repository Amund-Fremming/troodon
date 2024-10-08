namespace Test.Features.ICar;

public interface IICarRepository
{
    // Create
    Task<int> CreateAsync(ICar icar);

    // Read
    Task<ICar> GetByIdAsync(int id);

    // Update
    Task UpdateAsync(int id, ICar icar);

    // Delete
    Task DeleteAsync(int id);
}
