namespace Test.Features.Car;

public interface ICarRepository
{
    // Create
    Task<int> CreateAsync(Car car);

    // Read
    Task<Car> GetByIdAsync(int id);

    // Update
    Task UpdateAsync(int id, Car car);

    // Delete
    Task DeleteAsync(int id);
}
