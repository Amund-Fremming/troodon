namespace Test.Features.Car; 

    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<int> CreateAsync(Car car)
        {
            try
            {
                return await _carRepository.CreateAsync(car);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the car", ex);
            }
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            try
            {
                return await _carRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the car", ex);
            }
        }

        public async Task UpdateAsync(int id, Car car)
        {
            try
            {
                var existingCar = await _carRepository.GetByIdAsync(id);
                if (existingCar == null)
                {
                    throw new KeyNotFoundException("Car not found");
                }
                await _carRepository.UpdateAsync(id, car);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the car", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var existingCar = await _carRepository.GetByIdAsync(id);
                if (existingCar == null)
                {
                    throw new KeyNotFoundException("Car not found");
                }
                await _carRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the car", ex);
            }
        }
    }
