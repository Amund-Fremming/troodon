namespace Ragne.Features.to; 

    public class toService : ItoService
    {
        private readonly ItoRepository _toRepository;

        public toService(ItoRepository toRepository)
        {
            _toRepository = toRepository;
        }

        public async Task<Guid> CreateAsync(toModel toModel)
        {
            try
            {
                return await _toRepository.CreateAsync(toModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the toModel", ex);
            }
        }

        public async Task<toModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _toRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the toModel", ex);
            }
        }

        public async Task UpdateAsync(Guid id, toModel toModel)
        {
            try
            {
                var existingto = await _toRepository.GetByIdAsync(id);
                if (existingto == null)
                {
                    throw new KeyNotFoundException("to not found");
                }
                await _toRepository.UpdateAsync(id, toModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the toModel", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingto = await _toRepository.GetByIdAsync(id);
                if (existingto == null)
                {
                    throw new KeyNotFoundException("to not found");
                }
                await _toRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the toModel", ex);
            }
        }
    }
