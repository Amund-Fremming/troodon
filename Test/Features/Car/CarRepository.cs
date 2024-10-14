namespace Test.Features.Car; 

    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Car car)
        {
            try
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return car.Id; // Assuming Id is the primary key
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the car to the database", ex);
            }
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Cars.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the car from the database", ex);
            }
        }

        public async Task UpdateAsync(int id, Car car)
        {
            try
            {
                var existingCar = await _context.Cars.FindAsync(id);
                if (existingCar == null)
                {
                    throw new KeyNotFoundException("Car not found");
                }
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the car in the database", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var existingCar = await _context.Cars.FindAsync(id);
                if (existingCar == null)
                {
                    throw new KeyNotFoundException("Car not found");
                }
                _context.Cars.Remove(existingCar);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the car from the database", ex);
            }
        }
    }
