namespace Ragne.Features.tre; 

    public class treService : ItreService
    {
        private readonly ItreRepository _treRepository;

        public treService(ItreRepository treRepository)
        {
            _treRepository = treRepository;
        }

        public async Task<Guid> CreateAsync(treModel treModel)
        {
            try
            {
                return await _treRepository.CreateAsync(treModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the treModel", ex);
            }
        }

        public async Task<treModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _treRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the treModel", ex);
            }
        }

        public async Task UpdateAsync(Guid id, treModel treModel)
        {
            try
            {
                var existingtre = await _treRepository.GetByIdAsync(id);
                if (existingtre == null)
                {
                    throw new KeyNotFoundException("tre not found");
                }
                await _treRepository.UpdateAsync(id, treModel);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the treModel", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingtre = await _treRepository.GetByIdAsync(id);
                if (existingtre == null)
                {
                    throw new KeyNotFoundException("tre not found");
                }
                await _treRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the treModel", ex);
            }
        }
    }
