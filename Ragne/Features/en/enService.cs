namespace Ragne.Features.en; 

    public class enService : IenService
    {
        private readonly IenRepository _enRepository;

        public enService(IenRepository enRepository)
        {
            _enRepository = enRepository;
        }

        public async Task<Guid> CreateAsync(enModel enModel)
        {
            try
            {
                return await _enRepository.CreateAsync(enModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the enModel", ex);
            }
        }

        public async Task<enModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _enRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the enModel", ex);
            }
        }

        public async Task UpdateAsync(Guid id, enModel enModel)
        {
            try
            {
                var existingen = await _enRepository.GetByIdAsync(id);
                if (existingen == null)
                {
                    throw new KeyNotFoundException("en not found");
                }
                await _enRepository.UpdateAsync(id, enModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the enModel", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingen = await _enRepository.GetByIdAsync(id);
                if (existingen == null)
                {
                    throw new KeyNotFoundException("en not found");
                }
                await _enRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the enModel", ex);
            }
        }
    }
