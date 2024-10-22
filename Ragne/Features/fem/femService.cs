namespace Ragne.Features.fem; 

    public class femService : IfemService
    {
        private readonly IfemRepository _femRepository;

        public femService(IfemRepository femRepository)
        {
            _femRepository = femRepository;
        }

        public async Task<Guid> CreateAsync(femModel femModel)
        {
            try
            {
                return await _femRepository.CreateAsync(femModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the femModel", ex);
            }
        }

        public async Task<femModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _femRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the femModel", ex);
            }
        }

        public async Task UpdateAsync(Guid id, femModel femModel)
        {
            try
            {
                var existingfem = await _femRepository.GetByIdAsync(id);
                if (existingfem == null)
                {
                    throw new KeyNotFoundException("fem not found");
                }
                await _femRepository.UpdateAsync(id, femModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the femModel", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingfem = await _femRepository.GetByIdAsync(id);
                if (existingfem == null)
                {
                    throw new KeyNotFoundException("fem not found");
                }
                await _femRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the femModel", ex);
            }
        }
    }
