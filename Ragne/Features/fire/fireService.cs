namespace Ragne.Features.fire; 

    public class fireService : IfireService
    {
        private readonly IfireRepository _fireRepository;

        public fireService(IfireRepository fireRepository)
        {
            _fireRepository = fireRepository;
        }

        public async Task<Guid> CreateAsync(fireModel fireModel)
        {
            try
            {
                return await _fireRepository.CreateAsync(fireModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the fireModel", ex);
            }
        }

        public async Task<fireModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _fireRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the fireModel", ex);
            }
        }

        public async Task UpdateAsync(Guid id, fireModel fireModel)
        {
            try
            {
                var existingfire = await _fireRepository.GetByIdAsync(id);
                if (existingfire == null)
                {
                    throw new KeyNotFoundException("fire not found");
                }
                await _fireRepository.UpdateAsync(id, fireModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the fireModel", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingfire = await _fireRepository.GetByIdAsync(id);
                if (existingfire == null)
                {
                    throw new KeyNotFoundException("fire not found");
                }
                await _fireRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the fireModel", ex);
            }
        }
    }
