namespace Ragne.Features.seks; 

    public class seksService : IseksService
    {
        private readonly IseksRepository _seksRepository;

        public seksService(IseksRepository seksRepository)
        {
            _seksRepository = seksRepository;
        }

        public async Task<Guid> CreateAsync(seksModel seksModel)
        {
            try
            {
                return await _seksRepository.CreateAsync(seksModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the seksModel", ex);
            }
        }

        public async Task<seksModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _seksRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the seksModel", ex);
            }
        }

        public async Task UpdateAsync(Guid id, seksModel seksModel)
        {
            try
            {
                var existingseks = await _seksRepository.GetByIdAsync(id);
                if (existingseks == null)
                {
                    throw new KeyNotFoundException("seks not found");
                }
                await _seksRepository.UpdateAsync(id, seksModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the seksModel", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingseks = await _seksRepository.GetByIdAsync(id);
                if (existingseks == null)
                {
                    throw new KeyNotFoundException("seks not found");
                }
                await _seksRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the seksModel", ex);
            }
        }
    }
